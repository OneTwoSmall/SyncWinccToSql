using CCHMIRUNTIME;
using DBHelpClass.DBHelper;
using OpcUaHelper;
using ServiceInstaller.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using STimer = System.Timers;
using HslCommunication.Enthernet.Redis;
using CommonClass.CommonModel;
using System.IO;
using CommonClass.Helper;
using DBHelpClass.Helper;
using System.Linq;
using System.Data;
namespace ServiceInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region 弃用
        //private string servicePath = $"{Application.StartupPath}\\WindowsService.exe";
        //private string executePath = $"{Application.StartupPath}\\Services\\";//执行路径
        private string iniPath = $"{Application.StartupPath}\\Setting.INI";//执行路径

        private string servicePath = $"{Application.StartupPath}\\Services\\WindowsService.exe";//服务目录
        private string serviceName = "AWindowsService";
        private string matchIneName = System.Net.Dns.GetHostName();
        #endregion
        #region 新的
        private string _iniSettingFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Setting.INI";
        private string _sqlConnectString = string.Empty;
        private string _winccVersion = string.Empty;
        private string _redisIp = string.Empty;
        private int _redisPort = 0;
        private string _redisPass = string.Empty;

        /// <summary>
        /// 每天同步
        /// </summary>
        private STimer.Timer timer1 = new STimer.Timer();
        /// <summary>
        /// 重启服务
        /// </summary>
        private STimer.Timer reSyncTimer = new STimer.Timer();

        /// <summary>
        /// 实时读取 1分钟
        /// </summary>
        private STimer.Timer realSyncTimer = new STimer.Timer();

        /// <summary>
        /// opcua Client
        /// </summary>
        private OpcUaClient _opcUaClient = new OpcUaClient();
        /// <summary>
        /// 设备状态本地变量
        /// </summary>
        private Dictionary<string, List<StateDeviceModel>> DicDeviceStateList = new Dictionary<string, List<StateDeviceModel>>();
        /// <summary>
        /// redis Client
        /// </summary>
        private RedisClient _redisClient = null;

        private int archiveInterval = 0;

        private bool syncFlag = false;

        private static HMIRuntime hmiRuntime = null;
        #endregion

        /// <summary>
        /// 安装服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_install_Click(object sender, EventArgs e)
        {
            //已经存在
            if (this.IsServiceExisted(serviceName))
            {
                LoadServiceState("卸载中...");
                await UninstallService(servicePath);//卸载
            }
            LoadServiceState("安装中...");
            await this.InstallService(servicePath);
            LoadServiceState("已安装");
        }

        public static void SetWinccConnectionString()
        {
            //初始化wincc接口参数
            HMIRuntime hmiRuntime = new HMIRuntime();
            string winccSqlServerName = hmiRuntime.Tags["@DatasourceNameRT"].Read();
            string winccPcName = hmiRuntime.Tags["@ServerName"].Read();

            DbHelperOleDb.connectionString0 = "Provider=WinCCOLEDBProvider.1;Catalog=" + winccSqlServerName +
                                              ";Data Source=" + winccPcName + "\\WinCC";
            DbHelperOleDb.connectionString1 =
                "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" +
                winccSqlServerName + ";Data Source=" + winccPcName + "\\WinCC";

            DbHelperOleDb.connectionString2 =
           "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" +
           winccSqlServerName.TrimEnd('R') + ";Data Source=" + winccPcName + "\\WinCC";
        }
        private async Task InstallService(string path)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = path;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
            await Task.Delay(1000);
        }

        private async Task UninstallService(string path)
        {
            if (File.Exists(path))
            {
                using (AssemblyInstaller installer = new AssemblyInstaller())
                {
                    installer.UseNewContext = true;
                    installer.Path = path;
                    installer.Uninstall(null);
                }
            }
            await Task.Delay(1000);
        }

        private async Task ServiceStart(string name)
        {
            using (ServiceController control = new ServiceController(name))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
            await Task.Delay(1000);
        }

        private async Task ServiceStop(string name)
        {
            using (ServiceController control = new ServiceController(name))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
            await Task.Delay(1000);
        }


        private bool IsServiceExisted(string serviceName)
        {
            bool res = false;
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController item in services)
            {
                if (item.ServiceName.ToLower() == serviceName.ToLower())
                {
                    res = true;
                }
            }
            return res;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_start_Click(object sender, EventArgs e)
        {
            LoadServiceState("正在启动...");
            InitSyncDate isd = new InitSyncDate();
            isd.ShowDialog();
            if (this.IsServiceExisted(serviceName))
            {
                await ServiceStart(serviceName);
            }
            else
            {
                MessageBox.Show("请先安装服务");
            }
            LoadServiceState(GetServiceState(serviceName));
        }

        private async void btn_stop_Click(object sender, EventArgs e)
        {
            //LoadServiceState("正在停止...");
            //if (this.IsServiceExisted(serviceName))
            //    await ServiceStop(serviceName);
            //LoadServiceState(GetServiceState(serviceName));
            OnStop();
            LoadServiceState("服务已停止...");
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_unistall_Click(object sender, EventArgs e)
        {
            LoadServiceState("卸载中...");
            if (this.IsServiceExisted(serviceName))
                await UninstallService(servicePath);
            LoadServiceState(GetServiceState(serviceName));
        }

        private void LoadServiceState(string stateString)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LoadServiceState), stateString);
                return;
            }
            lb_ServiceState.Text = stateString;

        }

        private void LoadSyncInfo(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LoadSyncInfo), msg);
                return;
            }
            if (txt_Info.Lines.Count() > 100)
            {
                txt_Info.Clear();
            }
            txt_Info.Text = $"[{DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff")}]:{msg}\r\n{txt_Info.Text}";
        }

        bool winccReady = false;
        private async void Form1_Load(object sender, EventArgs e)
        {
            //LoadServiceState(GetServiceState(serviceName));
            //wincc初始化设置
            try
            {
                SetWinccConnectionString();
                winccReady = true;
                txt_Info.Text = "Wincc数据接口已经启动\r\n" + txt_Info.Text + "";
            }
            catch (Exception ex)
            {
                txt_Info.Text = "Wincc数据接口未启动\r\n" + txt_Info.Text + "";
                LogHelper.WriteLog("Wincc数据接口未启动");
            }
            LoadServiceState("卸载旧服务中...");
            if (this.IsServiceExisted(serviceName))
                await UninstallService(servicePath);
            LoadServiceState(GetServiceState(serviceName));
        }

        private string GetServiceState(string sname)
        {
            var state = "初始化...";
            try
            {
                using (ServiceController control = new ServiceController(sname))
                {
                    //if (!this.IsServiceExisted(serviceName))
                    //    state = "未安装";
                    //else 
                    if (control.Status == ServiceControllerStatus.Paused)
                    {
                        state = "已暂停";
                    }
                    else if (control.Status == ServiceControllerStatus.Stopped)
                    {
                        state = "已停止";
                    }
                    else if (control.Status == ServiceControllerStatus.StopPending)
                    {
                        state = "正在停止";
                    }
                    else if (control.Status == ServiceControllerStatus.StartPending)
                    {
                        state = "正在启动";
                    }
                    else if (control.Status == ServiceControllerStatus.Running)
                    {
                        state = "运行中";
                    }
                }
            }
            catch (Exception)
            {
                state = "未安装";
            }

            return state;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //using (ServiceController control = new ServiceController(sname))
            {

            }
        }
        public string Description
        {
            get
            {
                //construct the management path
                string path = "Win32_Service.Name='" + this.serviceName + "'";
                ManagementPath p = new ManagementPath(path);
                //construct the management object
                ManagementObject ManagementObj = new ManagementObject(p);
                if (ManagementObj["Description"] != null)
                {
                    return ManagementObj["Description"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// This property returns the Service startup type,
        /// it can be one of these values
        /// Automatic , Manual and Disabled
        /// and accept the same values
        /// </summary>

        public string StartupType
        {
            get
            {
                if (this.serviceName != null)
                {
                    //construct the management path
                    string path = "Win32_Service.Name='" + this.serviceName + "'";
                    ManagementPath p = new ManagementPath(path);
                    //construct the management object
                    ManagementObject ManagementObj = new ManagementObject(p);
                    return ManagementObj["StartMode"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value != "Automatic" && value != "Manual" && value != "Disabled")
                    throw new Exception("The valid values are Automatic, Manual or Disabled");

                if (this.serviceName != null)
                {
                    //construct the management path
                    string path = "Win32_Service.Name='" + this.serviceName + "'";
                    ManagementPath p = new ManagementPath(path);
                    //construct the management object
                    ManagementObject ManagementObj = new ManagementObject(p);
                    //we will use the invokeMethod method of the ManagementObject class
                    object[] parameters = new object[1];
                    parameters[0] = value;
                    ManagementObj.InvokeMethod("ChangeStartMode", parameters);
                }
            }
        }

        /// <summary>
        /// 设置数据链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 设置wincc数据过程归档变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (!winccReady)
            {
                try
                {
                    SetWinccConnectionString();
                    winccReady = true;
                    txt_Info.Text = "Wincc数据接口已经启动\r\n" + txt_Info.Text + "";
                }
                catch (Exception ex)
                {
                    txt_Info.Text = "Wincc数据接口未启动\r\n" + txt_Info.Text + "";
                    LogHelper.WriteLog("Wincc数据接口未启动");
                    return;
                }
            }

            WinccArchiveList wal = new WinccArchiveList();
            wal.ShowDialog();
        }

        /// <summary>
        /// 设置wincc 用户归档变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!winccReady)
            {
                try
                {
                    SetWinccConnectionString();
                    winccReady = true;
                }
                catch (Exception ex)
                {
                    txt_Info.Text = "Wincc数据接口未启动\r\n" + txt_Info.Text + "";
                    LogHelper.WriteLog("Wincc数据接口未启动");
                    return;
                }
            }

        }

        /// <summary>
        /// 设定同步的时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 编辑配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", iniPath);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread.Sleep(20);

            String path = AppDomain.CurrentDomain.BaseDirectory + "MAutoUpdate.exe";
            //同时启动自动更新程序
            if (File.Exists(path))
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                {
                    FileName = "MAutoUpdate.exe",
                    Arguments = " ServiceInstaller 0"//1表示静默更新 0表示弹窗提示更新
                };
                Process proc = Process.Start(processStartInfo);
                if (proc != null)
                {
                    proc.WaitForExit();

                }
            }
            ;
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        bool Init()
        {
            try
            {
                //归档周期设置
                var archiveInterval = INIHelper.ReadInteger("Wincc归档设置", "过程归档周期", 0, _iniSettingFilePath);
                if (archiveInterval == 0)
                {
                    LogHelper.WriteLog("过程归档同步周期设置为空，自动设置为50分钟");
                    archiveInterval = 50 * 60 * 1000;
                    INIHelper.WriteInteger("Wincc归档设置", "过程归档周期", archiveInterval, _iniSettingFilePath);
                }

                //wincc版本设置
                _winccVersion = INIHelper.ReadString("Wincc归档设置", "WinccVersion", "", _iniSettingFilePath);
                if (string.IsNullOrEmpty(_winccVersion))
                {
                    LogHelper.WriteLog("Wincc版本号为空，设置为7.4");
                    _winccVersion = "7.4";
                    INIHelper.WriteString("Wincc归档设置", "WinccVersion", _winccVersion, _iniSettingFilePath);
                }
                //数据库链接设置
                _sqlConnectString = INIHelper.ReadString("Wincc归档设置", "SqlConnction", "", _iniSettingFilePath);
                if (string.IsNullOrEmpty(_sqlConnectString))
                {
                    LogHelper.WriteLog("Sql数据库链接配置为空，无法执行同步");
                    INIHelper.WriteString("Wincc归档设置", "SqlConnction", "", _iniSettingFilePath);
                    return false;
                }
                //redis设置
                _redisIp = INIHelper.ReadString("Wincc归档设置", "RedisIp", "", _iniSettingFilePath);
                if (string.IsNullOrEmpty(_redisIp))
                {
                    LogHelper.WriteLog("redis配置读取失败，无法输出程序运行状态");
                    INIHelper.WriteString("Wincc归档设置", "RedisIp", "", _iniSettingFilePath);
                }
                _redisPort = INIHelper.ReadInteger("Wincc归档设置", "RedisPort", 0, _iniSettingFilePath);
                if (_redisPort == 0)
                {
                    INIHelper.WriteInteger("Wincc归档设置", "RedisPort", 6379, _iniSettingFilePath);
                }
                _redisPass = INIHelper.ReadString("Wincc归档设置", "RedisPass", "", _iniSettingFilePath);
                if (string.IsNullOrEmpty(_redisPass))
                {
                    INIHelper.WriteString("Wincc归档设置", "RedisPass", "", _iniSettingFilePath);
                }
                _redisClient = new RedisClient(_redisIp, _redisPort, _redisPass);

                LogHelper.WriteLog("Init服务开始");

                //timer 绑定事件

                timer1.Elapsed += timer1_Tick;
                reSyncTimer.Elapsed += reSyncTimer_Tick;
                //wincc初始化设置
                try
                {
                    SetWinccConnectionString();
                }
                catch (Exception ex)
                {
                    LoadServiceState("Wincc数据接口未启动,服务会自动重启...");
                    txt_Info.Text = "Wincc数据接口未启动\r\n" + txt_Info.Text + "";
                    LogHelper.WriteLog("Wincc数据接口未启动");
                    reSyncTimer.Interval = 10 * 60 * 1000;//十分钟重新初始化wincc读取接口设置
                    reSyncTimer.Start();//出错后重试
                    timer1.Stop();//停止运行数据读取
                    return false;

                }
                reSyncTimer.Stop();//停止重连计时器

                timer1.Interval = Convert.ToInt32(archiveInterval);
                timer1.Start();


                LogHelper.WriteLog("设备状态Redis初始化成功");
                return true;


            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog("Init", ex);
                return false;

            }
        }

        /// <summary>
        /// 重启初始化读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reSyncTimer_Tick(object sender, EventArgs e)
        {
            Init();
        }
        object lockObj = new object();
        /// <summary>
        /// 同步变量定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Hour == 1 && syncFlag)
                {
                    lock (lockObj)
                    {
                        LogHelper.WriteLog("开始同步前一天历史数据");
                        ReadWinccArchiveHis();
                        LogHelper.WriteLog("结束同步");
                    }
                    syncFlag = false;
                }
                if (DateTime.Now.Hour != 1)
                {
                    syncFlag = true;
                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                LogHelper.WriteLog("同步出错:" + ex.Message);
                Init();
            }
        }

        /// <summary>
        /// /启动任务，打开定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartSync_Click(object sender, EventArgs e)
        {
            OnStart();
        }
        public void OnStart()
        {
            LogHelper.WriteLog("服务开始");
            LoadServiceState("服务开始...");
            //服务启动时，直接执行语句
            if (Init())
            {

                realSyncTimer.Interval = 5 * 60 * 1000;//5分钟读取实际值
                realSyncTimer.Elapsed += RealSyncTimer_Elapsed;
                realSyncTimer.Start();//出错后重试
            }
            LoadServiceState("运行中...");
            btn_StartSync.Enabled = false;
        }

        CancellationTokenSource oleDbHelperCancelTokenSource = new CancellationTokenSource();

        private void ReadRealValue()
        {
            try
            {
                var syncCount = INIHelper.ReadBoolean("Wincc归档设置", "实时读取", false, _iniSettingFilePath);
                if (!syncCount)
                {
                    return;
                }
                SqlHelper.connectionString = _sqlConnectString;
                hmiRuntime = new HMIRuntime();
                //获取需要实时读取的变量的值
                var realvalueTable = SqlHelper.ExecuteDataSet(_sqlConnectString, CommandType.Text, "Select * from RealValue", null);
                if (realvalueTable == null || realvalueTable.Tables.Count == 0)
                {
                    LogHelper.WriteLog("realvalueTable");

                    return;
                }
                var archiveDt = (DataTable)realvalueTable.Tables[0];
                //读取wincc变量表所有valueid
                foreach (DataRow dr in archiveDt.Rows)
                {
                    try
                    {
                        var tagName = dr["ValueName"] + "";
                        dr["RealValue"] = hmiRuntime.Tags[tagName].Read() + "";
                        dr["SyncTime"] = DateTime.Now;
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }

                SqlHelper.SqlBulkUpdateByDatatable("RealValue", archiveDt, "Id", "Id");
            }
            catch (Exception ex)//异常但是不用抛出
            {
                //log异常现象
            }

        }
        public void ReadWinccArchiveHis()
        {
            try
            {
                LogHelper.WriteLog("开始同步数据Wincc数据");
                LoadSyncInfo("开始同步数据Wincc数据");
                var startString = INIHelper.ReadString("Wincc归档设置", "manualStartTime", "", _iniSettingFilePath);
                var endtString = INIHelper.ReadString("Wincc归档设置", "manualEndTime", "", _iniSettingFilePath);
                DateTime? startTime = null;
                DateTime? endTime = null;
                if (!string.IsNullOrEmpty(startString))
                {
                    startTime = Convert.ToDateTime(startString);
                }
                if (!string.IsNullOrEmpty(endtString))
                {
                    endTime = Convert.ToDateTime(endtString);
                }

                //var sqlConnetString = INIHelper.ReadString("Wincc归档设置", "SqlConnection", "", _iniSettingFilePath);
                SqlHelper.connectionString = _sqlConnectString;
                if (string.IsNullOrEmpty(_sqlConnectString))
                {
                    LogHelper.WriteLog("_sqlConnectString未配置");
                    LoadSyncInfo("_sqlConnectString未配置");
                    return;
                }
                var winccArchiveTable = SqlHelper.ExecuteDataSet(_sqlConnectString, CommandType.Text, "Select * from Archive", null);
                if (winccArchiveTable == null || winccArchiveTable.Tables.Count == 0)
                {
                    LogHelper.WriteLog("winccArchiveTable未配置");
                    LoadSyncInfo("winccArchiveTable未配置");
                    return;
                }
                var archiveDt = (DataTable)winccArchiveTable.Tables[0];
                //读取wincc变量表所有valueid
                var valueIdList = archiveDt.Rows.Cast<DataRow>().Select(row => row.Field<int>("ValueID"));

                var currentPage = 0;
                var totalPages = 0;
                //读取每次额能同步的个数实时读取
                var syncCount = INIHelper.ReadInteger("Wincc归档设置", "每次同步个数", 0, _iniSettingFilePath);
                if (syncCount == 0)
                {
                    syncCount = 20;
                    INIHelper.WriteInteger("Wincc归档设置", "每次同步个数", syncCount, _iniSettingFilePath);
                }
                var pageSize = syncCount;
                totalPages = (int)Math.Ceiling((valueIdList.Count() / pageSize * 1M));

                //默认只获取前一整天 的历史数据 如有缺失 需要手动同步
                var foreDay_s = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                var foreDay_e = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                if (startTime != null && endTime != null)
                {
                    foreDay_s = startTime.Value;
                    foreDay_e = endTime.Value;
                }
                LoadSyncInfo($"开始同步[{foreDay_s:yy-MM-dd}]-[{foreDay_e:yy-MM-dd}]间的数据");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //for 重新每10个valueid,查询一次
                for (int i = 0; i < totalPages; i++)
                {
                    var partIds = valueIdList.Skip((i) * pageSize).Take(pageSize);
                    var splitids = string.Join(";", partIds);

                    var dealDt = GetWinccArchiveTableByValueIds(splitids, foreDay_s, foreDay_e);
                    SqlHelper.connectionString = _sqlConnectString;
                    //创建一个当前临时表，所有数据都插入这个表中，然后在进行处理
                    SqlHelper.CreateCurrentDataTable();
                    //创建完了就写入  写入后，在sql server 设置定时任务自动归档到每一天
                    if (dealDt != null && dealDt.Rows.Count > 0)
                    {
                        SqlHelper.SqlBulkCopyByDatatable("CurrentDayData", dealDt);
                        LogHelper.WriteLog($"本次写入{dealDt.Rows.Count}条记录");
                    }
                    //取消任务时 终止循环
                    if (oleDbHelperCancelTokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                }
                //服务启动执行完成后，把时间写为空，后面每天执行时同步数据即可
                INIHelper.WriteString("Wincc归档设置", "manualStartTime", "", _iniSettingFilePath);
                INIHelper.WriteString("Wincc归档设置", "manualEndTime", "", _iniSettingFilePath);
                sw.Stop();
                LoadSyncInfo($"结束同步[{foreDay_s}]-[{foreDay_e}]间的数据，耗时{sw.ElapsedMilliseconds / 1000}秒");
            }
            catch (Exception ex)
            {

                LogHelper.ErrorLog("同步数据Wincc数据失败", ex);
                LoadSyncInfo("同步数据Wincc数据失败" + ex);
            }
        }
        /// <summary>
        /// 通过wincc oledb接口获取过程归档数据
        /// </summary>
        /// <param name="valueIds"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        DataTable GetWinccArchiveTableByValueIds(string valueIds, DateTime st, DateTime et)
        {
            DataTable dt = null;
            //wincc存储时间为 utc 0 区时间   查询时 需要处理一下  - 8 Hours
            var sqlUpit1 =
                $"Tag:R,({valueIds}),'{st.AddHours(-8).AddSeconds(1):yyyy-MM-dd HH:mm:ss.fff}','{et.Date.AddHours(-8):yyyy-MM-dd HH:mm:ss.fff}'"; //0000-00-00 00:00:00.000
            try
            {
                //Task.Run(() => oleDbHelper.QueryTest(sqlUpit1, cancellationToken, winccVersion)); //获取查询变量的结果集
                var winccDataSet = DbHelperOleDb.Query(sqlUpit1);
                if (winccDataSet.Tables.Count > 0 && winccDataSet.Tables[0].Rows.Count > 0)
                {
                    var dealDt = CommonClass.Helper.WinccHandleHelper.SwapTable(winccDataSet.Tables[0], _winccVersion);
                    dt = dealDt;
                }
            }
            catch (AggregateException e)
            {
                e.Handle((x) => x is OperationCanceledException);
                throw;
            }
            return dt;
        }


        protected void OnStop()
        {
            oleDbHelperCancelTokenSource.Cancel();
            timer1.Stop();
            reSyncTimer.Stop();
            LogHelper.WriteLog("服务已停止");

        }

        object realLock = new object();
        private void RealSyncTimer_Elapsed(object sender, STimer.ElapsedEventArgs e)
        {

            //realSyncTimer.Stop();
            lock (realLock)
            {
                ReadRealValue();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("同步服务将会最小化到托盘", "系统提醒", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.ShowInTaskbar = false;
                    this.notifyIcon1.Visible = true;
                }
                if (this.WindowState != FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Minimized;
                }

                e.Cancel = true;
            }
        }
        FormWindowState fws = FormWindowState.Normal;
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
            else
            {
                fws = this.WindowState;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        /// <summary>
        /// 手动执行，选择时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ManuSync_Click(object sender, EventArgs e)
        {
            InitSyncDate isd = new InitSyncDate();
            isd.ShowDialog();
            //Thread.Sleep(10000);
            //服务启动时，直接执行语句
            if (Init())
            {
                Task.Run(() => ReadWinccArchiveHis());
                //读取实时变量
                Task.Run(() => ReadRealValue());

            }
            LoadSyncInfo("手工执行完成");
        }

        private void toolStripExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出本系统吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Application.ExitThread();
            }
        }
    }

}
