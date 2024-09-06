using System;
using System.Collections.Generic;
using ServiceInstaller.Model;

namespace ServiceInstaller.Bll
{
    public static class Bll_OpcUa
    {
        /// <summary>
        /// 获取opcua服务器信息
        /// </summary>
        /// <param name="roomName">车间名称</param>


        /// <summary>
        /// 获取OPC 节点信息
        /// </summary>
        /// <param name="opcUaId">opcua服务器id</param>
        /// 《<returns>opcuaNode列表</returns>
        public static IEnumerable<OpcUaModel.OpcUaNodeInfo> GetNodeInfo(int opcUaId)
        {
            if (opcUaId <= 0) throw new ArgumentOutOfRangeException(nameof(opcUaId));

            var dc = Dal.Dal_OpcUa.GetNodeInfo(opcUaId);
            //返回的表为空
            if (dc.Count == 0)
            {
                return null;
            }

            var list = new List<OpcUaModel.OpcUaNodeInfo>();
            return list;
        }
    }
}