
using System;
using System.Drawing;

namespace CommonClass.CommonModel
{
    /// <summary>
    /// 设备状态模型
    /// </summary>
    public class StateDeviceModel
    {
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

    }

    /// </summary>
    public class StateSumModel
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
        public int StateCount { get; set; }

        public Color StateColor { get; set; } = System.Drawing.Color.FromArgb(79, 129, 189);

    }


    public class KaiGongInfo
    {
        public int 设备号 { get; set; }
        public int 开工分钟数 { get; set; }
        public DateTime _TIMESTAMP { get; set; }

    }

    /// <summary>
    /// 同步状态
    /// </summary>
    public class SyncStateKV
    {

        public bool isSync { get; set; }

        public DateTime SyncTime { get; set; }




    }
}