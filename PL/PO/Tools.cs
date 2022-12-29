using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace PO;

static class Tools
{
    private static IBL bl = BLFactory.GetBL();
    internal static BO.Product CopyProductToBO (this PO.ProductPO prodPO)
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
}
