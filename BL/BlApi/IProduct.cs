using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetListedProducts(); //עבור מנהל ועבור קטלוג קונה .בקשת רשימת מוצרים

    public IEnumerable<BO.ProductForList> GetListedProductsWithDeleted();
    public BO.Product GetProducts(int id); //בקשת פרטי מוצר עבור מנהל
    public BO.ProductItem GetProducts(int id,BO.Cart cart); // בקשת פרטי מוצר עבור הקונה
    public void AddProduct(BO.Product product); //הוספת מוצר עבור מנהל
    public void DeleteProduct(int id); //מחיקת מוצר עבור מנהל
    public void UpdateProduct(BO.Product newproduct); //עידכון נתוני מוצר עבור מנהל

    //    public override string ToString() =>  Tools.ToStringProperty(this);
    public override BO.Product Clone() => Cloning.Clone(this);
    
    // public IEnumerable<ProductItem> GetProducts(int id); //בקשת פרטי מוצר עבור מנהל
    //public IEnumerable<ProductItem> GetProducts(); // בקשת פרטי מוצר עבור הקונה

}
