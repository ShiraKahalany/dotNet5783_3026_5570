
using DalApi;
using DO;
namespace Dal;
//namespace DalApi;

//מימוש ממשק המוצרים
public class DalProduct : IProduct
{
   DataSource dataSource = DataSource.s_instance;

    #region Add
    public int Add(Product item)
        //הוספת מוצר חדש לרשימת המוצרים, מחזירה את המזהה של המוצר החדש
    {

        if (item.ID >= 100000 && dataSource.Orders.Find(x => x?.ID == item.ID) == null)
        {
            dataSource.Products.Add(item);
            return item.ID;
        }
        item.ID = DataSource.Config.NextProductNumber;
        dataSource.Products.Add(item);
        return item.ID;
    }
    #endregion

    #region Update
    public void Update(Product item)
        //מתודה המעדכנת את המוצר עם ה "ז שהתקבל בהתאם למוצר המעודכן שהתקבל כפרמטר
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new DO.NotExistException("The item is not exist");
        DeletePermanently(item.ID);
        Add(item);
    }
    #endregion

    #region Restore
    public void Restore(Product item)
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new DO.NotExistException("The item is not deleted");
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }
    #endregion

    #region Delete Permanently
    public void DeletePermanently(int id)
        //מחיקה לצמיתות של מוצר
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        dataSource.Products.Remove(temp);
    }
    #endregion

    #region Delete
    public void Delete(int id)
        //העברת מוצר לארכיון -סימון כמחוק
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if(temp?.IsDeleted==true)
            throw new DO.NotExistException("The item is already deleted");
        dataSource.Products.Remove(temp);
        Product product=new Product { IsDeleted=true, Category=temp?.Category??0, InStock=temp?.InStock, Name=temp?.Name, Price=temp?.Price, ID= temp.GetValueOrDefault().ID, Path=temp?.Path};
        Add(product);
    }
    #endregion

    #region Get All The Products By Filter
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
            return dataSource.Products;
        var ieproducts = from product in dataSource.Products
                       where filter(product) == true
                       select product;
       
        return ieproducts;
    }
    #endregion

    #region Get Product By Filter
    public Product? GetTByFilter(Func<Product?, bool> filter)
    {
        DO.Product? item = dataSource.Products.Find((DO.Product? product) => filter(product));
        if (item == null)
            throw new DO.NotExistException("Not Exist - Product");
        return item;

    }
    #endregion
}
