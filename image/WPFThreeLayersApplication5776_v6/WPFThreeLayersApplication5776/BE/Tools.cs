using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }


        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }


        public static T GetCopy<T>(this T source) where T : new()
        {
            T result = new T();
            foreach (PropertyInfo item in source.GetType().GetProperties())
            {
                try
                {
                    if (item.CanWrite && item.CanRead) // (item.PropertyType.IsValueType)
                    {
                        object srcValue = item.GetValue(source, null);
                        item.SetValue(result, srcValue);
                    }
                    //else //if(item.PropertyType.IsClass)
                    //{
                    //    object srcValue = item.GetValue(source, null);
                    //    item.SetValue(result, srcValue.GetDeepCopy());
                    //}
                }
                catch (Exception e)
                {
                    Debug.WriteLine($" --->> err copy property {item.Name} from {source.GetType().Name} \n {e.Message}");
                }
            }
            return result;
        }

    }
}
