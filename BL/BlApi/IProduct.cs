using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;


//ממשק עבור מוצרים
public interface IProduct
{
    //public IEnumerable<BO.ProductForList?> GetListedProducts(); //בקשת רשימת מוצרים (קטלוג) עבור המנהל או הלקוח

    //public IEnumerable<BO.ProductForList?>? GetListedProductsWithDeleted();  // קבלת רשימת כל המוצרים שהיו או כעת בחנות
    //public IEnumerable<BO.ProductForList?>? GetListedDeletedProducts();  //קבלת רשימת כל המוצרים שנמחקו
    //public BO.Product? GetProduct(int id); //בקשת פרטי מוצר (עבור מנהל

    //public BO.Product? GetDeletedById(int id); //בקשת פרטי מוצר מחוק (עבור מנהל
    public void Restore(int id); //שיחזור מוצר מחוק (מנהל בלבד
    //public BO.Product GetProduct(int id);
   public BO.ProductItem? GetProductItem(int id, BO.Cart? cart); // בקשת פרטי מוצר (עבור הקונה
    public void AddProduct(BO.Product? product); //הוספת מוצר (עבור מנהל
    public void DeleteProduct(int id); //מחיקת מוצר (עבור מנהל
    public void UpdateProduct(BO.Product? newproduct); //עידכון נתוני מוצר (עבור מנהל
    //public IEnumerable<BO.ProductForList> GetByCategory(BO.Category ct);
    public IEnumerable<BO.ProductForList> GetProductList(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null);
    public IEnumerable<BO.Product> GetProducts(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null);
    // public IEnumerable<BO.ProductItem> GetKatalog();  //בקשת קטלוג עבור הקונה
    //    public override string ToString() =>  Tools.ToStringProperty(this);

    // public IEnumerable<ProductItem> GetProducts(int id); //בקשת פרטי מוצר עבור מנהל
    //public IEnumerable<ProductItem> GetProducts(); // בקשת פרטי מוצר עבור הקונה
    public IEnumerable<BO.ProductItem> GetProductItemsList(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null);

}
