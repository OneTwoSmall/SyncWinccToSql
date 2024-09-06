using System.Configuration;

namespace ServiceInstaller.Dal
{
    class Dal_DevInfo
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;
    }
}