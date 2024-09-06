using System;
using System.Data;

namespace CommonClass.Helper
{
    public class WinccHandleHelper
    {
        //ValueName                VarType
        //测试变量类型\有符号32位	 3
        //测试变量类型\有符号16位	 2
        //测试变量类型\有符号8位	 16
        //测试变量类型\无符号32位	 19
        //测试变量类型\无符号16位	 18
        //测试变量类型\无符号8位	 17
        //测试变量类型\文本16位	 8
        //测试变量类型\文本8位	     8
        //测试变量类型\布尔类型	 11
        //测试变量类型\浮点64位	 5
        //测试变量类型\浮点32位	 4
        public static Type GetDateType(int varType)
        {
            switch (varType)
            {
                case 2:
                    return typeof(int);
                case 3:
                    return typeof(int);
                case 4:
                    return typeof(float);
                case 5:
                    return typeof(float);
                case 7:
                    return typeof(string);
                case 8:
                    return typeof(string);
                case 11:
                    return typeof(bool);
                case 16:
                    return typeof(int);
                case 17:
                    return typeof(int);
                case 18:
                    return typeof(int);
                case 19:
                    return typeof(int);
                default:
                    return typeof(string);
            }

        }

        /// <summary>
        /// wincc7.4 数据库归档名称转化为7.3版本
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static DataTable SwapTable(DataTable dataTable, string winccVer)
        {
            if (Convert.ToDecimal(winccVer) < 7.4M || !dataTable.Columns.Contains("TimeStampExt"))
            {
                return dataTable;
            }
            DataTable dt = dataTable.Copy();
            dt.Columns.Remove("TimeStampExt");//移除此列数据
            dt.Columns["VariantValue"].ColumnName = "RealValue";
            return dt;
        }
    }
}
