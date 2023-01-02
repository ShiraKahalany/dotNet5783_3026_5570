using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BlApi;
using System.Windows.Media;

namespace PLConverter;

public class NotVisibilityToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Visibility visibilityValue = (Visibility)value;
        if (visibilityValue == Visibility.Hidden)
        {
            return Visibility.Visible; //Visibility.Collapsed;
        }
        else
        {
            return Visibility.Hidden;
        }
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Visibility visibilityValue = (Visibility)value;
        if (visibilityValue == Visibility.Hidden)
        {
            return Visibility.Visible; //Visibility.Collapsed;
        }
        else
        {
            return Visibility.Hidden;
        }
    }
}


public class FalseToTrueConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (boolValue)
        {
            return false; //Visibility.Collapsed;
        }
        else
        {
            return true;
        }
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (boolValue)
        {
            return false; //Visibility.Collapsed;
        }
        else
        {
            return true;
        }
    }
}

public class FalseToTrueConverterDataGrid : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (boolValue && parameter is DataGrid && !(parameter as DataGrid).IsGrouping)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (boolValue && parameter is DataGrid && !(parameter as DataGrid).IsGrouping)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class CategoryToStringConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((BO.Category)value)
        {
            case Category.Kitchen:
                return "Kitchen";
            case Category.Garden:
                return "Garden";
            case Category.Living_room:
                return "Living Room";
            case Category.Bedroom:
                return "Bed Room";
            case Category.Bathroom:
                return "Bath Room";
            case Category.All:
                return "All Products";
            default:
                return " ";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return " ";
    }
}
public class BoolToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        if (boolValue)
        {
            return Visibility.Visible; //Visibility.Collapsed;
        }
        else
        {
            return Visibility.Hidden;
        }
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Visibility visibilityValue = (Visibility)value;
        if (visibilityValue is Visibility.Visible)
        {
            return true; //Visibility.Collapsed;
        }
        else
        {
            return false;
        }
    }
}

   public class IntToStringPhoneConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch ((BO.Category)value)
        {
            case Category.Kitchen:
                return "Kitchen";
            case Category.Garden:
                return "Garden";
            case Category.Living_room:
                return "Living Room";
            case Category.Bedroom:
                return "Bed Room";
            case Category.Bathroom:
                return "Bath Room";
            case Category.All:
                return "All Products";
            default:
                return " ";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return " ";
    }
}

public class IntToStringConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        if (value is not "") { return int.Parse(value.ToString()); }

        else return null;
    }
}

public class StringConverterTodouble : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return value.ToString();
    }
}

public class StringConverterToIntBattery : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (int.Parse(value.ToString()) > 100)
            return 100;
        else return int.Parse(value.ToString());
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {

        return value.ToString();
    }
}
public class TextToBool : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.ToString() == "")
            return false;
        else return true;

    }



    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return "";
    }
}

public class FalseToTrueUpdateConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value.ToString() == "" && parameter.ToString() == "")
        {
            return false; //Visibility.Collapsed;
        }
        else
        {
            return true;
        }
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //never used
        return "";
    }
}





