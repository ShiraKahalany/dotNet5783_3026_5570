using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
namespace BlApi;

//מתודות הרחבה
public static class Tools
{
    public static string ToStringProperty<T>(this T t, string suffix = "")
        //מתודה להפיכת ישות למחרוזת לצורך הצגת הפרטים
    {
        string str = "";
        foreach (PropertyInfo prop in t.GetType().GetProperties())
        {
            if (prop.Name == "IsDeleted")
            {
                bool? val = (bool?)prop.GetValue(t, null);
                if (val??false)
                    str += " * * * DELETED * * *:";
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

    public static Target CopyFields<Source, Target>(this Source source, Target target)
    {

        if (source is not null && target is not null)
        {
            Dictionary<string, PropertyInfo> propertiesInfoTarget = target.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p);

            IEnumerable<PropertyInfo> propertiesInfoSource = source.GetType().GetProperties();

            foreach (var propertyInfo in propertiesInfoSource)
            {
                if (propertiesInfoTarget.ContainsKey(propertyInfo.Name)
                    && (propertyInfo.PropertyType == typeof(string) || !(propertyInfo.PropertyType.IsClass)))
                {
                    propertiesInfoTarget[propertyInfo.Name].SetValue(target, propertyInfo.GetValue(source));
                }
            }
        }
        return target;
    }

    public static object CopyPropToStruct<S>(this S from, Type type)//get the typy we want to copy to 
    {
        object to = Activator.CreateInstance(type); // new object of the Type
        from.CopyFields(to);//copy all value of properties with the same name to the new object
        return to;
    }


    //public static V CopyFields<T, V>(this T from, V to)
    ////מתודה להעתקת שדות עם שם זהה בין שתי ישויות שונות
    //{
    //    foreach (PropertyInfo propTo in to.GetType().GetProperties())
    //    {

    //        PropertyInfo propFrom = from.GetType().GetProperty(propTo.Name);
    //        if (propFrom == null)
    //            continue;
    //        var value = propFrom.GetValue(from, null);
    //        if (value is ValueType || value is string)
    //            propTo.SetValue(to, value);
    //    }


        //foreach (PropertyInfo propFrom in from.GetType().GetProperties())
        //    {



        //        if (propTo.Name == propFrom.Name)
        //            propTo.SetValue(from, propFrom, null);
        //    }
        //}
        //return to;
    }



