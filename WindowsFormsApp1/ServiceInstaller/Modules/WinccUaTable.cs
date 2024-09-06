using CCHMIRUNTIME;
using DBHelpClass.DBHelper;
using System;
using System.Data;
using System.Windows.Forms;

namespace ServiceInstaller.Modules
{
    public partial class WinccUaTable : Form
    {
        public WinccUaTable()
        {
            InitializeComponent();
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
        private void WinccUaTable_Load(object sender, EventArgs e)
        {
            SetWinccConnectionString();
            QueryUaArchiveFromWincc();
        }

        private void QueryUaArchiveFromWincc()
        {
            string sqlQuery = $"SELECT * FROM Archive";
            DataTable winccArchiveList = DbHelperOleDb.Query1(sqlQuery).Tables[0];
            dgv_UA_Main.DataSource = winccArchiveList;
        }


    }
}
