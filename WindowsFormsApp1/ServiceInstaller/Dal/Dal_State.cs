using System.Configuration;

namespace ServiceInstaller.Dal
{
    public class Dal_State
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionString = ConfigurationManager.ConnectionStrings[""].ConnectionString;


    }
}
