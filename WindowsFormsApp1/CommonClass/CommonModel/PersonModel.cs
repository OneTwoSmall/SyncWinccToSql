using System;

namespace CommonClass.CommonModel
{
    public class PersonModel
    {
        /// <summary>
        /// 当班人员
        /// </summary>
        public class DutyPsnModel
        {
            public int Did { get; set; }
            /// <summary>
            /// 班长
            /// </summary>
            public string ClassMaster { get; set; }
            /// <summary>
            /// 班组人员
            /// </summary>
            public string PsnName { get; set; }

            /// <summary>
            /// 照片
            /// </summary>
            public byte[] Pic { get; set; }

            /// <summary>
            /// 班次  早班、晚班
            /// </summary>
            public string ClassType { get; set; }
            /// <summary>
            /// 班次时间
            /// </summary>
            public string ClassTime { get; set; }
        }

        /// <summary>
        /// 节假日值班
        /// </summary>
        public class HolidayDutyPsnModel
        {
            public int Id { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string PsnName { get; set; }

            /// <summary>
            /// 照片
            /// </summary>
            public byte[] Pic { get; set; }

            /// <summary>
            /// 照片
            /// </summary>
            public string PicString { get; set; }

            /// <summary>
            /// 联系方式
            /// </summary>
            public string Contact { get; set; }
            ///// <summary>
            ///// 时间
            ///// </summary>
            //public DateTime StartTime { get; set; }

            ///// <summary>
            ///// 结束时间
            ///// </summary>
            //public DateTime EndTime { get; set; }
        }


        /// <summary>
        /// 外来加工
        /// </summary>
        public class OuterWorkderModel
        {

            public int Id { get; set; }
            /// <summary>
            /// 公司
            /// </summary>
            public string Company { get; set; }
            /// <summary>
            /// 负责人
            /// </summary>
            public string ResponsePsn { get; set; }
            /// <summary>
            /// 联系方式
            /// </summary>
            public string Contact { get; set; }

            /// <summary>
            /// 人数
            /// </summary>
            public int Count { get; set; }

            /// <summary>
            /// 施工内容
            /// </summary>
            public string WorkContent { get; set; }

            /// <summary>
            /// 时间
            /// </summary>
            public DateTime StartTime { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EndTime { get; set; }
        }
    }
}
