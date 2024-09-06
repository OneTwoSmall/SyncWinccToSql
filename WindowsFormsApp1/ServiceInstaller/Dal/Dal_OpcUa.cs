using DBHelpClass.Helper;
using System.Data;

namespace ServiceInstaller.Dal
{
    /// <summary>
    /// opc ua DAL类
    /// </summary>
    public class Dal_OpcUa
    {
        //private static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static string connString = "Data Source=192.168.0.213;Initial Catalog=FLH_JZ_Monitor;User ID=sa;Password=elco@2015";
        /// <summary>
        /// 获取opcua服务器信息
        /// </summary>
        /// <param name="roomName">车间名称</param>
        ///  <returns>返回DataTableCollection</returns>
        public static DataTableCollection GetOpcUaServerInfo(string roomName)
        {
            var sql =
                $@"SELECT dbo.Sys_RoomOpcUaInfo.Id,  dbo.Sys_RoomOpcUaInfo.DbId, dbo.Sys_RoomOpcUaInfo.EndPointURL, dbo.Sys_RoomOpcUaInfo.SecurityPolicy, 
            dbo.Sys_Database.RoomName, dbo.Sys_Database.ConnectionString, dbo.Sys_Database.ProviderName
                FROM      dbo.Sys_Database INNER JOIN
            dbo.Sys_RoomOpcUaInfo ON dbo.Sys_Database.Id = dbo.Sys_RoomOpcUaInfo.DbId where RoomName='{roomName}'";
            SqlHelper.connectionString = connString;
            var res = SqlHelper.GetTable(CommandType.Text, sql, null);

            return res;
        }

        /// <summary>
        /// 获取OPC 节点信息
        /// </summary>
        public static DataTableCollection GetNodeInfo(int opcUaId)
        {
            var sql = $@" SELECT 
                                      [OpcUaId]
                                      ,[ValueBelongs]
                                      ,[TagName]
                                      ,[Address]
                                      ,[DataType]
                            FROM [dbo].[Sys_DeviceNodeInfo] 
                                where opcUaId={opcUaId}";
            SqlHelper.connectionString = connString;
            var res = SqlHelper.GetTable(CommandType.Text, sql, null);

            return res;
        }
    }
}