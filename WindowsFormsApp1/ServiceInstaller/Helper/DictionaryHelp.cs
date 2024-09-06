using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ServiceInstaller.Helper
{
    public static class DictionaryHelp
    {
        /// <summary>  
        /// 将对象属性转换为key-value对  
        /// </summary>  
        /// <param name="o"></param>  
        /// <returns></returns>  
        public static Dictionary<string, object> ToDictionary(object o)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new object[] { }));
                }
            }

            return map;

        }
        public static object GetObject(Dictionary<string, object> dict, Type type)
        {
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                var prop = type.GetProperty(kv.Key);
                if (prop == null) continue;

                object value = kv.Value;
                if (string.IsNullOrWhiteSpace(value + ""))//如果读取到“”则将value设置为NULL
                {
                    value = null;
                }
                if (value is Dictionary<string, object>)
                {
                    value = GetObject((Dictionary<string, object>)value, prop.PropertyType); // <= This line
                }
                if (value != null && value.GetType() + "" == "System.Single")
                {
                    value = Math.Round(Convert.ToDouble(value), 3); // <= This line
                }
                if (value != null && value.ToString().Count(x => x.Equals('-')) == 2)//有两个-的表示是日期时间字符串需要格式化
                {
                    try
                    {
                        value = Convert.ToDateTime(value);
                    }
                    catch
                    {
                        value = "";
                    }

                }
                //prop.SetValue(obj, value, null);
                prop.SetValue(obj, value);
            }
            return obj;
        }
        public static T GetObject<T>(Dictionary<string, object> dict)
        {
            return (T)GetObject(dict, typeof(T));
        }
    }
}