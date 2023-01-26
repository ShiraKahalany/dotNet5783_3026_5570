using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace PL;

public static class Tools
{
    private static IBL bl = BLFactory.GetBL();

    public static BO.Product CopyProductToBO(this PO.ProductPO prodPO)
        //מתודה המקבלת מוצר מסוג פו, ומחזירה עותק מסוג בו
    {
        BO.Product copyProduct = new()
        {
            IsDeleted = prodPO.IsDeleted,
            ID = prodPO.ID,
            Name = prodPO.Name,
            InStock = prodPO.InStock,
            Path = prodPO.Path,
            Price = prodPO.Price,
            Category = prodPO.Category
        };
        return copyProduct;
    }

    public static BO.OrderItem CopyOrderItemToBO(this PO.OrderItemPO orPO)
        //מתודה המקבלת מוצר-בהזמנה מסוג פו, ומחזירה עותק מסוג בו
    {
        BO.OrderItem copyOrderItem = new()
        {
            IsDeleted = orPO.IsDeleted,
            ID = orPO.ID,
            Name = orPO.Name,
            TotalItem = orPO.TotalItem,
            ProductID = orPO.ProductID,
            Amount = orPO.Amount,
            Path = orPO.Path,
            Price = orPO.Price
        };
        return copyOrderItem;
    }


    public static ObservableCollection<U> ToObservableByConverter<T, U>(this IEnumerable<T> ienumcollect, ObservableCollection<U> observablecollect, Func<T, U> converter)
        //מתודה גנרית להמרה מאוסף לאובזרבל, כאשר כל עצם מועתק עי קונברטר שהתקבל
    {
        if(observablecollect==null)
            observablecollect=new();
        else
                    observablecollect.Clear();
        if (ienumcollect == null)
            return new ObservableCollection<U>();
        foreach (T item in ienumcollect)
        {
            observablecollect.Add(converter(item));
        }
        return observablecollect;
    }

    public static void EnterNumbersOnly(object sender, KeyEventArgs e)
        //מתודה להגדרת תקיות קלט - רק מספרים
    {
        TextBox? text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;

        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;

        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
         || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
            return;

        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

        //allow control system keys
        if (Char.IsControl(c)) return;

        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox

        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
        return;
    }


    public static void EnterNumbersOrPointOnly(object sender, KeyEventArgs e)
        //מתודה להגדרת תקינות קלט- רק מספרים או נקודה
    {
        TextBox? text = sender as TextBox;
        if (text == null) return;
        if (e == null) return;

        //allow get out of the text box
        if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            return;

        //allow list of system keys (add other key here if you want to allow)
        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||  e.Key==Key.OemPeriod ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
         || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
            return;

        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

        //allow control system keys
        if (Char.IsControl(c)) return;

        //allow digits (without Shift or Alt)
        if (Char.IsDigit(c))
            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                return; //let this key be written inside the textbox

        //forbid letters and signs (#,$, %, ...)
        e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
        return;
    }

    public static Target CopyFields<Source, Target>(this Source source, Target target)
        //מתודת גנרית להעתקת ערכי שדות זהים
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

    public static PO.OrderPO BoToPoOrder(BO.Order source)
        //מתודה המקבלת הזמנה מסוג בו ומחזירה עותק מסוג פו
    {
        PO.OrderPO po = new PO.OrderPO();
        po=source.CopyFields<BO.Order,PO.OrderPO>(po);
        po.Items = (from item in source.Items
                   select CopyProp<BO.OrderItem,PO.OrderItemPO>(item)).ToList();
        return po;
    }

    public static T CopyProp<S, T>(S from)//get the typy we want to copy to 
        //מתודה גנרית המקבלת עצם מקור ומחזירה עצם עם שדות מועתקים
    {
        Type t = typeof(T);
        object to = Activator.CreateInstance(t)!; // new object of the Type
        from.CopyFields(to);//copy all value of properties with the same name to the new object
        return (T)to;
    }

    public static IEnumerable<BO.OrderItem?> ObservableToIEnumerable(this ObservableCollection<PO.OrderItemPO> observablecollect)
        //מתודה המעתיקה אוסף לאובזרבל
    {
        var x = from PO.OrderItemPO item in observablecollect
                select item.CopyOrderItemToBO();
        return x;
    }

    public static PO.CartPO CopyBOCartToPO(this BO.Cart cartBO)
        //מתודה המקבלת עגלה מסוג בו ומחזירה עותק מסוג פו
    {
        PO.CartPO copycart = new PO.CartPO
        {
            CustomerAddress = cartBO.CustomerAddress,
            CustomerEmail = cartBO.CustomerEmail,
            CustomerName = cartBO.CustomerName,
            TotalPrice = cartBO.TotalPrice
        };
        copycart.Items = cartBO.Items!.ToObservableByConverter<BO.OrderItem, PO.OrderItemPO>(copycart.Items!, CopyProp<BO.OrderItem, PO.OrderItemPO>);
        return copycart;
    }
}
