using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace Dal;

internal static class Cloning 
//משבטת באופן אוטומטי כל טיפוס ב DO
{
    internal static T Clone<T>(this T original) where T :new()   // internal static V Clone??
    {
        T copyToObject =new T();
        foreach(PropertyInfo propertyInfo in typeof (T).GetProperties())
            propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original,null), null);
        return copyToObject;
    }
}
