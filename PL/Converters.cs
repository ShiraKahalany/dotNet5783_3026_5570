using BO;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace PLConverter;


public class IsEmptyToNotVisibility2Converter : IValueConverter ///ours ,picture
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((int)value == 0)
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

public class IsEmptyToVisible2Converter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((int)value ==0)
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



public class BoolToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolvalue = (bool)value;
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

public class AmountToVisibilityConverter : IValueConverter //used
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((int)value == 0)
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
        if ((int)value > 0)
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
        if (((BO.OrderStatus)value == BO.OrderStatus.Shipped) || ((BO.OrderStatus)value == BO.OrderStatus.Delivered))
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


public class StatusToColorConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if ((BO.OrderStatus)value == BO.OrderStatus.Ordered)
            return Brushes.Fuchsia;
        if ((BO.OrderStatus)value == BO.OrderStatus.Shipped)
            return Brushes.DarkViolet;
        if ((BO.OrderStatus)value == BO.OrderStatus.Delivered)
            return Brushes.Blue;
        return Brushes.Black;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return 1;
    }
}


public class IntToVisibilityConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if ((int)value <= 0)
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
public class AllTextBoxesFilled7Converter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (value != values.Last() && string.IsNullOrWhiteSpace((string)value))
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


public class ProgressToIntConverter : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime now = (DateTime)parameter;
        if (((PO.OrderPO)value).Status == BO.OrderStatus.Ordered)
        {
            DateTime? date = ((PO.OrderPO)value).OrderDate;
            TimeSpan? diff = now - date;
            int days = diff?.Days ?? 0;
            return days - 31;
        }
        if (((PO.OrderPO)value).Status == BO.OrderStatus.Shipped)
        {
            DateTime? date = ((PO.OrderPO)value).ShipDate;
            TimeSpan? diff = now - date;
            int days = diff?.Days ?? 0;
            return days - 14;
        }
        if (((PO.OrderPO)value).Status == BO.OrderStatus.Delivered)
        {
            DateTime? date = ((PO.OrderPO)value).OrderDate;
            TimeSpan? diff = now - date;
            int days = diff?.Days ?? 0;
            return days - 7;
        }
        return 0;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //try { int.Parse(value.ToString()); }
        return 1;
    }
}

public class StatusToValue : IValueConverter
{
    //convert from source property type to target property type
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if ((BO.OrderStatus)value == BO.OrderStatus.Ordered)
            return 33;
        if ((BO.OrderStatus)value == BO.OrderStatus.Shipped)
            return 66;
        if ((BO.OrderStatus)value == BO.OrderStatus.Delivered)
            return 100;
        return 0;
    }

    //convert from target property type to source property type
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 1;
    }
}




public class IsEnabledToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Visibility)value == Visibility.Visible;
    }
}

public class OpacityToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((Visibility)value== Visibility.Visible) ? 0.4 : 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Visibility) Visibility.Visible;
    }
}

public class NotVisibilityToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((Visibility)value==Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Visibility)Visibility.Visible;
    }
}

