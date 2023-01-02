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
    internal static BO.Product CopyProductToBO(this PO.ProductPO prodPO)
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

    public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> ienumcollect, ObservableCollection<T> observablecollect)
    {
        observablecollect.Clear();
        foreach (T item in ienumcollect)
        {
            observablecollect.Add(item);
        }
        return  observablecollect;
    }
}
