using CCHMIRUNTIME;
using CommonClass.Helper;
using DBHelpClass.DBHelper;
using DBHelpClass.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace ServiceInstaller.Modules
{
    public partial class WinccArchiveList : Form
    {
        public WinccArchiveList()
        {
            InitializeComponent();
        }


        public static bool SetWinccConnectionString()
        {
            //初始化wincc接口参数
            HMIRuntime hmiRuntime = new HMIRuntime();
            string winccSqlServerName = hmiRuntime.Tags["@DatasourceNameRT"].Read();
            string winccPcName = hmiRuntime.Tags["@ServerName"].Read();
            if (string.IsNullOrEmpty(winccSqlServerName) || string.IsNullOrEmpty(winccPcName))
            {
                MessageBox.Show("Wincc没有启动 或 Wincc Connectivity包未安装！");
                return false;
            }
            DbHelperOleDb.connectionString0 = "Provider=WinCCOLEDBProvider.1;Catalog=" + winccSqlServerName +
                                              ";Data Source=" + winccPcName + "\\WinCC";
            DbHelperOleDb.connectionString1 =
                "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" +
                winccSqlServerName + ";Data Source=" + winccPcName + "\\WinCC";

            DbHelperOleDb.connectionString2 =
           "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=" +
           winccSqlServerName.TrimEnd('R') + ";Data Source=" + winccPcName + "\\WinCC";

            return true;
        }
        private void WinccArchiveList_Load(object sender, EventArgs e)
        {
            SetWinccConnectionString();

            QueryArchiveFromWincc();
        }

        void QueryArchiveFromWincc()
        {
            string sqlQuery = $"SELECT *,'' as ValueType,Convert(bit,1) as IsCollected FROM Archive";
            DataTable winccArchiveList = DbHelperOleDb.Query1(sqlQuery).Tables[0];
            //处理一下vartype ，数据类型
            foreach (DataRow dr in winccArchiveList.Rows)
            {
                var valueType = WinccHandleHelper.GetDateType((int)dr["VarType"]);
                dr["ValueType"] = valueType;
            }
            dataGridView1.DataSource = winccArchiveList;
            dataGridView1.Columns["ValueId"].ReadOnly = true;

        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (SetWinccConnectionString())
            {
                QueryArchiveFromWincc();
            }
        }

        /// <summary>
        /// 导入服务器数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var _sqlConnectString = INIHelper.ReadString("Wincc归档设置", "SqlConnction", "", BaseClass._iniSettingFilePath);
            if (string.IsNullOrEmpty(_sqlConnectString))
            {
                INIHelper.WriteString("Wincc归档设置", "SqlConnction", "", BaseClass._iniSettingFilePath);
                MessageBox.Show("Wincc2Sql 链接字符串[SqlConnction]没有配置！！！请配置后重试");

                return;
            }
            SqlHelper.connectionString = _sqlConnectString;
            //创建表
            if (SqlHelper.CreateTableToSqlServer((DataTable)dataGridView1.DataSource, "Archive"))
            {
                SqlHelper.SqlBulkUpdateByDatatable("Archive", (DataTable)dataGridView1.DataSource, "ValueID");
            }
        }
    }
}
