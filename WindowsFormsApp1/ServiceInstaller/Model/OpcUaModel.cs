using System.Collections.Generic;

namespace ServiceInstaller.Model
{
    public class OpcUaModel
    {
        /// <summary>
        /// opcua 服务期名称
        /// </summary>
        public string OpcUaServerName { get; set; }

        /// <summary>
        /// opcua 变量分组字典
        /// </summary>
        public Dictionary<string, string[]> OpcUaGroupDic { get; set; }

        /// <summary>
        /// opcua节点信息
        /// </summary>
        public class OpcUaNodeInfo
        {
            /// <summary>
            /// opcua Server id
            /// </summary>
            public int OpcUaId { get; set; }



            /// <summary>
            /// 节点所属设备号
            /// </summary>
            public string ValueBelongs { get; set; }

            /// <summary>
            /// 节点名称
            /// </summary>
            public string TagName { get; set; }

            /// <summary>
            /// 节点地址
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 数据类型
            /// </summary>
            public string DataType { get; set; }
        }

        /// <summary>
        /// opcua 服务器信息
        /// </summary>
        public class OpcUaServerInfo
        {
            /// <summary>
            /// 车间名称
            /// </summary>
            public string RoomName { get; set; }
            /// <summary>
            /// 主键id
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 端点URL地址
            /// </summary>
            public string EndPointURL { get; set; }

            /// <summary>
            /// 安全策略
            /// </summary>
            public string SecurityPolicy { get; set; }

            /// <summary>
            /// 消息模式
            /// </summary>
            public string MsgModel { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// 超时时间
            /// </summary>
            public string TimeOut { get; set; }
        }
    }
}