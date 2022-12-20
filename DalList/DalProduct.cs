
using DalApi;
using DO;
namespace Dal;
//namespace DalApi;

//מימוש ממשק המוצרים
public class DalProduct : IProduct
{
    DataSource dataSource = DataSource.s_instance;
  public int Add(Product item)
        //מתחודה שמקבלת מוצר ומוסיפה אותו אל רשימת כל המוצרים
    {
        if (dataSource.Products.Find(x => x?.ID == item.ID) != null)
            throw new DO.AlreadyExistException("This number is in use");
        if(item.ID >= 100000) // בדיקת תקינות המספר המזהה
        {
            dataSource.Products.Add(item);
            return item.ID;
        }

        Random rand = new Random();
        int newID = 0;
        do newID = rand.Next(100000, 999999);
        while (dataSource.Products.Find(x => x?.ID == newID)!=null);
        item.ID=newID;
        dataSource.Products.Add(item);
        return item.ID;
    }
    //public Product GetByID(int id)
    //    //מתודה המקבלת מספר ת"ז ומחזירה את המוצר המתאים לה
    //{
    //    var x = from item in dataSource.Products
    //            where item?.IsDeleted == false && item?.ID == id
    //            select item;
    //    return (Product)x.FirstOrDefault();       
    //}

    //public Product GetDeletedById(int id)
    //{
    //    var x= from item in dataSource.Products
    //           where item?.ID == id
    //           select (Product)item;
    //    if(!x.Any())
    //        throw new MyExceptionNotExist("The item is not exist");
    //    if (x.FirstOrDefault().IsDeleted==false)
    //        throw new MyExceptionNotExist("The item is not deleted");
    //    return(Product)x.FirstOrDefault();
    //}

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

    public void DeletePermanently(int id)
        //מחיקה לצמיתות של מוצר
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        dataSource.Products.Remove(temp);
    }
    public void Delete(int id)
        //העברת מוצר לארכיון -סימון כמחוק
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new DO.NotExistException("The item is not exist");
        if(temp?.IsDeleted==true)
            throw new DO.NotExistException("The item is already deleted");
        dataSource.Products.Remove(temp);
        Product product=new Product { IsDeleted=true, Category=temp?.Category??0, InStock=temp?.InStock, Name=temp?.Name, Price=temp?.Price, ID= temp.GetValueOrDefault().ID};
        Add(product);
    }

    //public IEnumerable<Product?> GetAll()
    //    //מתודה המחזירה את רשימת כל המוצרים
    //{
    //    List<Product?> listGet = new List<Product?>();

    //    foreach (Product? item in dataSource.Products)
    //    {if(item?.IsDeleted==false) listGet.Add((Product)item); }
    //    return listGet;
    //}

    //public IEnumerable<Product?> GetAllWithDeleted()
    ////מתודה המחזירה את רשימת כל המוצרים, כולל אלו שנחמקו
    //{
    //    List<Product?> listGet = new List<Product?>();
    //    foreach (Product? item in dataSource.Products) {listGet.Add((Product)item); }
    //    return listGet;
    //}

    //public IEnumerable<Product?> GetAllDeleted()
    //{
    //    List<Product?> listGet = new List<Product?>();
    //    foreach (Product? item in dataSource.Products) { if (item?.IsDeleted == true) listGet.Add((Product)item); }
    //    return listGet;
    //}

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
            return dataSource.Products;
        var ieproducts = from product in dataSource.Products
                       where filter(product) == true
                       select product;
        //if (!ieorderitems.Any())   //???
        //    throw new DO.NotExistException("Not Exist");///??
        return ieproducts;
    }

    public Product? GetTByFilter(Func<Product?, bool> filter)
    {
        var x = (from product in dataSource.Products
                 where filter(product) == true
                 select product).First();
        if (x == null)
            throw new DO.NotExistException("Not Exist");
        return x;
    }
}
