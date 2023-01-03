using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Collections.ObjectModel;


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
