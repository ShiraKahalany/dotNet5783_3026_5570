using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlApi;


//ממשק עבור מוצרים
public interface IProduct
{

    public void Restore(int id); //שיחזור מוצר מחוק (מנהל בלבד
    public BO.Product GetById(int id); //קבלת הזמנה לפי מס מזהה
   public BO.ProductItem? GetProductItem(int id, BO.Cart? cart); // בקשת פרטי מוצר (עבור הקונה
    public int AddProduct(BO.Product? product); //הוספת מוצר (עבור מנהל
    public void DeleteProduct(int id); //מחיקת מוצר (עבור מנהל
    public void UpdateProduct(BO.Product? newproduct); //עידכון נתוני מוצר (עבור מנהל
   public IEnumerable<BO.Product> GetProducts(BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null);  //קבלת רשימה מסוג Product לפי פילטר
    public IEnumerable<BO.ProductItem> GetProductItemsList(BO.Cart cart, BO.Filters enumFilter = BO.Filters.None, Object? filterValue = null );  //קבלת רשימה מסוג ProductItem לפי פילטר

}
