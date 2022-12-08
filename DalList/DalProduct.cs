
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
        if(item.ID >= 1000 && dataSource.Products.Find(x => x?.ID == item.ID) == null)
        {
            dataSource.Products.Add(item);
            return item.ID;
        }

        Random rand = new Random();
        int newID = 0;
        do
        {
            newID = rand.Next(100000, 999999);
        }
        while (dataSource.Products.Find(x => x?.ID == newID)!=null);
        item.ID=newID;

        //Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        //if (temp != null&& temp?.IsDeleted == false)
        //    throw new MyExceptionAlreadyExist("The item already exists");
        dataSource.Products.Add(item);
        //dataSource.Products.Add(new Product { ID = item.ID, IsDeleted = false, Price=item.Price, Category=item.Category, InStock=item.InStock, Name=item.Name}); ;
        return item.ID;

    }
    public Product GetByID(int id)
        //מתודה המקבלת מספר ת"ז ומחזירה את המוצר המתאים לה
    {
        foreach (Product? item in dataSource.Products) { if (item?.IsDeleted==false && item?.ID == id) return (Product)item; }
        throw new MyExceptionNotExist("The item is not exist");
    }

    public Product GetDeletedById(int id)
    {
        foreach (Product? item in dataSource.Products)
        {
            if (item?.ID == id)
            {
                if(item?.IsDeleted == false)
                    throw new MyExceptionNotExist("The item is not deleted");
                return (Product)item;
            }
        }

        throw new MyExceptionNotExist("The item is not exist");
    }
    public void Update(Product item)
        //מתודה המעדכנת את המוצר עם ה "ז שהתקבל בהתאם למוצר המעודכן שהתקבל כפרמטר
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == true)
            throw new MyExceptionNotExist("The item is not exist");
        item.IsDeleted = true;
        DeletePermanently(item.ID);
        Add(item);
    }

    public void Restore(Product item)
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new MyExceptionNotExist("The item is not deleted");
        DeletePermanently(item.ID);
        item.IsDeleted = false;
        Add(item);
    }

    public void DeletePermanently(int id)
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if (temp?.IsDeleted == false)
            throw new MyExceptionNotExist("The item is not deleted - cant delete permanently");
        dataSource.Products.Remove(temp);
    }
    public void Delete(int id)
        //מתודה המוחקת אץת המוצר על ה ת"ז שהתקבלה
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new MyExceptionNotExist("The item is not exist");
        if(temp?.IsDeleted==true)
            throw new MyExceptionNotExist("The item is already deleted");
        dataSource.Products.Remove(temp);
        Product product=new Product { IsDeleted=true, Category=temp?.Category??0, InStock=temp?.InStock, Name=temp?.Name, Price=temp?.Price, ID= temp.GetValueOrDefault().ID};
        Add(product);
    }


    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    public IEnumerable<Product> GetAll()
        //מתודה המחזירה את רשימת כל המוצרים
    {
        List<Product> listGet = new List<Product>();
        foreach (Product? item in dataSource.Products) {if(item?.IsDeleted==false) listGet.Add((Product)item); }
        return listGet;
    }
//מתודה המחזירה את רשימת כל המוצרים, כולל אלו שנחמקו
    public IEnumerable<Product> GetAllWithDeleted()
    //מתודה המחזירה את רשימת כל המוצרים, כולל אלו שנחמקו
    {
        List<Product> listGet = new List<Product>();
        foreach (Product? item in dataSource.Products) {listGet.Add((Product)item); }
        return listGet;
    }

    public IEnumerable<Product> GetAllDeleted()
    {
        List<Product> listGet = new List<Product>();
        foreach (Product? item in dataSource.Products) { if (item?.IsDeleted == true) listGet.Add((Product)item); }
        return listGet;
    }

}
