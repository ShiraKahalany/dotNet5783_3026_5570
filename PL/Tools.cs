using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;


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
        return  observablecollect;
    }

    public static ObservableCollection<U> ToObservableByConverter<T,U>(this IEnumerable<T> ienumcollect, ObservableCollection<U> observablecollect, Func<T,U> converter)
    {
        observablecollect.Clear();
        foreach (T item in ienumcollect)
        {
            observablecollect.Add(converter(item));
        }
        return observablecollect;
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


    public static T CopyProp<S,T>(S from)//get the typy we want to copy to 
    {
        Type t = typeof(T);
        object to = Activator.CreateInstance(t)!; // new object of the Type
        from.CopyFields(to);//copy all value of properties with the same name to the new object
        return (T)to;
    }

    public static IEnumerable<BO.OrderItem?> ObservableToIEnumerable (this ObservableCollection<PO.OrderItemPO> observablecollect )
    {
        var x= from PO.OrderItemPO item in observablecollect
        select item.CopyOrderItemToBO();    
        return x;
    }

    public static BO.Cart CopyPOCartToBO(this PO.CartPO cartPO)
    {
        BO.Cart copycart = new()
        {
            CustomerAddress = cartPO.CustomerAddress,
            CustomerEmail =cartPO.CustomeEmail,
            CustomerName = cartPO.CustomerName,
            TotalPrice = cartPO.TotalPrice
        };
        copycart.Items = cartPO.Items!.ObservableToIEnumerable().ToList();
        return copycart;
    }
}
