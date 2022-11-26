using BlogDictionaryToList.CustomAttribute;
using BlogDictionaryToList.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogDictionaryToList
{
    public static class StaticConverter
    {
        /// <summary>
        /// Convert Dictionary To List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="ColumnMatchTable"> var columnMatchTable = new Dictionary<string, string>() { {"Subject", "Konu" } }; </param>
        /// <returns></returns>
        public static List<T> DictionaryToList<T>(this IEnumerable<Dictionary<string, object>> dictionary, 
            Dictionary<string, string> ColumnMatchTable)
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            List<T> list = new List<T>();
            Dictionary<string, bool> AttributeDictionaryList = new Dictionary<string, bool>(); //NEW
            foreach (Dictionary<string, object> dict in dictionary)
            {
                T model = Activator.CreateInstance<T>();

                string modelName = model.GetType().ToString();//NEW

                foreach (KeyValuePair<string, object> dic in dict)
                {
                    //uniqueKey is Ignore Property
                    if (dic.Key != "uniqueKey") //Unique Dictionary Key
                    {
                        //PropertyInfo propertyInfo = model.GetType().GetProperty(dic.Key);
                        PropertyInfo propertyInfo = model.GetType().GetProperty(CheckedMapedKey(dic.Key, ColumnMatchTable));

                        Type t = propertyInfo.PropertyType;
                        t = Nullable.GetUnderlyingType(t) ?? t;
                        object safeValue;

                        //if (Attribute.IsDefined(propertyInfo, typeof(JsonData)))
                        if (CheckAttribute(modelName, propertyInfo, typeof(JsonData), AttributeDictionaryList))//NEW
                            safeValue = (dic.Value == null || dic.Value == System.DBNull.Value) ? null : dic.Value.ToJson();
                        else
                            safeValue = (dic.Value == null || dic.Value == System.DBNull.Value) ? null : Convert.ChangeType(dic.Value, t);

                        propertyInfo.SetValue(model, safeValue, null);
                    }
                }
                list.Add(model);
            }
            Console.WriteLine("Total Time:" + watch.Elapsed);
            return list;
        }

        public static bool CheckAttribute(string modelName, PropertyInfo pi, Type type, Dictionary<string, bool> AttributeDictionaryList) //NEW
        {
            if (AttributeDictionaryList.ContainsKey(modelName + "." + pi.Name + "." + type.Name))
                return AttributeDictionaryList[modelName + "." + pi.Name + "." + type.Name];
            else
            {
                var result = Attribute.IsDefined(pi, type);
                AttributeDictionaryList.Add(modelName + "." + pi.Name + "." + type.Name, result);
                return result;
            }
        }

        public static string CheckedMapedKey(string key, Dictionary<string, string> ColumnMatchTable)
        {
            if (ColumnMatchTable.Count > 0)
            {
                if (ColumnMatchTable.TryGetValue(key, out string value))
                {
                    return value;
                }
            }
            return key;
        }
    }   
}
