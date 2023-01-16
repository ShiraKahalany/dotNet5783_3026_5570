using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Controls;
namespace PL;

public static class Tools
{
    private static IBL bl = BLFactory.GetBL();
    public static BO.Product CopyProductToBO(this PO.ProductPO prodPO)
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
    {
        BO.OrderItem copyOrderItem = new()
        {
            IsDeleted = orPO.IsDeleted,
            ID = orPO.ID,
            Name = orPO.Name,
            ProductID = orPO.ProductID,
            Amount = orPO.Amount,
            Path = orPO.Path,
            Price = orPO.Price
        };
        return copyOrderItem;
    }


    public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> ienumcollect, ObservableCollection<T> observablecollect)
    {
        observablecollect.Clear();
        foreach (T item in ienumcollect)
        {
            observablecollect.Add(item);
        }
        return observablecollect;
    }

    public static ObservableCollection<U> ToObservableByConverter<T, U>(this IEnumerable<T> ienumcollect, ObservableCollection<U> observablecollect, Func<T, U> converter)
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
    {
        TextBox text = sender as TextBox;
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

    public static Target CopyProperties<Source, Target>(Source source, Target target)
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


    public static T CopyProp<S, T>(S from)//get the typy we want to copy to 
    {
        Type t = typeof(T);
        object to = Activator.CreateInstance(t)!; // new object of the Type
        from.CopyFields(to);//copy all value of properties with the same name to the new object
        return (T)to;
    }

    public static IEnumerable<BO.OrderItem?> ObservableToIEnumerable(this ObservableCollection<PO.OrderItemPO> observablecollect)
    {
        var x = from PO.OrderItemPO item in observablecollect
                select item.CopyOrderItemToBO();
        return x;
    }

    public static BO.Cart CopyPOCartToBO(this PO.CartPO cartPO)
    {
        BO.Cart copycart = new BO.Cart
        {
            CustomerAddress = cartPO.CustomerAddress,
            CustomerEmail = cartPO.CustomerEmail,
            CustomerName = cartPO.CustomerName,
            TotalPrice = cartPO.TotalPrice
        };
        copycart.Items = cartPO.Items!.ObservableToIEnumerable().ToList();
        return copycart;
    }

    public static PO.CartPO CopyBOCartToPO(this BO.Cart cartBO)
    {
        PO.CartPO copycart = new PO.CartPO
        {
            CustomerAddress = cartBO.CustomerAddress,
            CustomerEmail = cartBO.CustomerEmail,
            CustomerName = cartBO.CustomerName,
            TotalPrice = cartBO.TotalPrice
        };
        copycart.Items = cartBO.Items.ToObservableByConverter<BO.OrderItem, PO.OrderItemPO>(copycart.Items, CopyProp<BO.OrderItem, PO.OrderItemPO>);
        //copycart.Items = cartPO.Items!.ObservableToIEnumerable().ToList();
        return copycart;
    }

    public static void AddToPOCart(this PO.CartPO cart, PO.OrderItemPO item, int amountToAdd = 1)
    {
        if (cart.Items.Contains(item))
        {
            cart.Items.Remove(item);
            item.Amount += amountToAdd;
            cart.Items.Add(item);
        }
        else
        {
            item.Amount += amountToAdd;
            cart.Items.Add(item);
        }
        cart.TotalPrice+=item.Price*amountToAdd;
        cart.TotalPrice = Math.Round(cart.TotalPrice??0, 2);
    }

}
