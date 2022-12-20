using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace DO;

public static class Tools
{
    public static string ToStringProperty<T>(this T t) where T : struct =>

    t.GetType().GetProperties()
     .Aggregate("", (str, prop) => str + "\n" + prop.Name + ": " + prop.GetValue(t, null));

    //public static string ToStringProperty<T>(T t)
    //{
    //    string str = "";
    //    foreach (PropertyInfo item in t.GetType().GetProperties())
    //        str += "\n" + item.Name
    //+ ": " + item.GetValue(t, null);
    //    return str;
    //}

}
