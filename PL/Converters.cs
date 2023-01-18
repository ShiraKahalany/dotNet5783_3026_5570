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
using System.Collections.ObjectModel;


namespace PLConverter;
using PO;

public class NotVisibilityToVisibilityConverter : IValueConverter //used
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


public class FalseToTrueConverter : IValueConverter //used
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

public class IsEmptyToNotVisibilityConverter : IValueConverter ///ours ,picture
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((ObservableCollection<PO.OrderItemPO>)value).Any())
            return Visibility.Hidden;
        else
            return Visibility.Visible;

    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null!;
    }
}

public class IsEmptyToVisibleConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((ObservableCollection<PO.OrderItemPO>)value).Any())
            return Visibility.Visible;
        else
            return Visibility.Hidden;

    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null!;
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
        bool boolvalue =(bool)value;
        if (boolvalue)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
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


public class AmountToVisibilityConverter : IValueConverter //used
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if((int)value==0)
            return Visibility.Visible; //Visibility.Collapsed;
        else return Visibility.Collapsed;
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



public class IsInStockToStringConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
            return "In stock";
        else return "NotInStock";
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return false;
    }
}


public class IsInStockToColorConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        if ((bool)value)
            return Brushes.Green;
        else
            return Brushes.Red;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return false;
    }
}


public class BoolToIsEnabledConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
return (bool)value;
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

public class IntToStringConverter : IValueConverter //used
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((int)value).ToString();
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}



public class BoolToVisibility2Converter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((int)value>0)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}


public class ShippedToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((BO.OrderStatus)value == BO.OrderStatus.Shipped)||((BO.OrderStatus)value == BO.OrderStatus.Delivered))
            return Visibility.Visible;
        else
            return Visibility.Hidden;
        
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}

public class ShippedToNotVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (((BO.OrderStatus)value == BO.OrderStatus.Shipped) || ((BO.OrderStatus)value == BO.OrderStatus.Delivered))
            return Visibility.Hidden;
        else
            return Visibility.Visible;

    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}


public class DeliveredToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((BO.OrderStatus)value == BO.OrderStatus.Delivered)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}


public class DeliveredToNotVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((BO.OrderStatus)value == BO.OrderStatus.Delivered)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }


    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return false;
    }
}


public class AmountToColorConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if ((int)value==0)
            return Brushes.LightGray;
        else
            return Brushes.LightGreen;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return 1;
    }
}

public class TimeToColorConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value == null)
            return "";
        else
            return ((DateTime)value).ToString();
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return DateTime.Now;
    }
}

public class IntToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if ((int)value<=0)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}


class TextBoxesFilledConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values.All(x => !string.IsNullOrWhiteSpace(x as string));
    }
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


class AllTextBoxesFilledConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var textboxes = parameter as TextBox[];
        if (textboxes == null) return true;
        return textboxes.All(x => !string.IsNullOrWhiteSpace(x.Text)) && !string.IsNullOrWhiteSpace((string)value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


public class AllTextBoxesFilled7Converter : IMultiValueConverter
{
    //public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //{
    //    foreach (var value in values)
    //    {
    //        if (string.IsNullOrWhiteSpace((string)value))
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (value != values.Last()&& string.IsNullOrWhiteSpace((string)value))
            {
                return false;
            }
            if (value == null && value == values.Last())
            {
                return false;
            }
        }
        return true;
    }
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
