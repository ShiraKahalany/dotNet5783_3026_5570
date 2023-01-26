using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace DO;

public static class Tools
{
    #region ToStringProperty
    public static string ToStringProperty<T>(this T t) where T : struct =>

    t.GetType().GetProperties()
     .Aggregate("", (str, prop) => str + "\n" + prop.Name + ": " + prop.GetValue(t, null));
    #endregion
}
