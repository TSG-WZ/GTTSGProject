using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace DTTSG_Common
{
    /// <summary>
    /// 对象字典互转类
    /// </summary>
    public static class ModelExtension
    {
        /// <summary>
        /// 获取对象的属性和值
        /// </summary>
        /// <typeparam name="T">范型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>属性与值对应的字典</returns>
        public static Dictionary<string, string> ToDictionary<T>(this T obj)
        {
            if (obj != null)
            {
                Dictionary<string, string> propertyValue = new Dictionary<string, string>();
                Type type = obj.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();
                var KEY = propertyInfos.Where(p => p.GetCustomAttributes(typeof(T), false).Length > 0).FirstOrDefault();
                
                foreach (PropertyInfo item in propertyInfos)
                {
                    var _PropertyValue = (item.GetValue(obj, null) ?? "").ToString();
                    if (!_PropertyValue.StartsWith("DTTSG") && item.Name != KEY.Name)
                    {
                        propertyValue.Add(item.Name, _PropertyValue);

                    }
                    
                }
                propertyValue.Add("Key", (KEY.GetValue(obj, null) ?? "").ToString());
                
                return propertyValue;
            }
            return null;
        }

        /// <summary>
        /// 字典转对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="dic">字典</param>
        /// <returns>对象模型</returns>
        public static T DicToObject<T>(this Dictionary<string, object> dic) where T:new()
        {
            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                var filed = textInfo.ToTitleCase(d.Key);
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(filed).SetValue(md, value);
                }
                catch (Exception ex)
                {

                }
            }
            return md;
        }
    }
}