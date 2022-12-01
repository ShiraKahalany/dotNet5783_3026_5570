using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;


namespace BO;

static class Tools
{
    public static string ToStringProperty<T>(this T t, string suffix = "")
    {
        string str = "";
        foreach (PropertyInfo prop in t.GetType().GetProperties())
        {
            if (prop.Name == "IsDeleted")
            {
                bool? val = (bool?)prop.GetValue(t, null);
                if (val??false)
                    str += "\t * * * DELETED * * *: \n";
                continue;
            }
            var value = prop.GetValue(t, null);
            if (value is not string && value is IEnumerable)
            {
                str = str +"\n"+ prop.Name + ":";
                foreach (var item in (IEnumerable)value)
                    str += item.ToStringProperty("      ");
            }

            else
                str += "\n" + suffix + prop.Name + ": " + value;
        }
        str += "\n";
        return str;
    }


    public static V CopyFields<T, V>(this T from, V to)
    {
        foreach (PropertyInfo propTo in to.GetType().GetProperties())
        {

            PropertyInfo propFrom = from.GetType().GetProperty(propTo.Name);
            if (propFrom == null)
                continue;
            var value = propFrom.GetValue(from, null);
            if (value is ValueType || value is string)
                propTo.SetValue(to, value);
        }





        //foreach (PropertyInfo propFrom in from.GetType().GetProperties())
        //    {



        //        if (propTo.Name == propFrom.Name)
        //            propTo.SetValue(from, propFrom, null);
        //    }
        //}
        return to;
    }

}
