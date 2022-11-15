
using DalApi;
using DO;
namespace Dal;
//namespace DalApi;

public class DalProduct:IProduct
{
    DataSource dataSource = DataSource.s_instance;
    int Add(Product item)
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        if (temp == null)
            throw new Exception("The product already exists");
        dataSource.Products.Add(item);
        //dataSource.Products.Add(new Product { ID = item.ID, IsDeleted = false, Price=item.Price, Category=item.Category, InStock=item.InStock, Name=item.Name}); ;
        return item.ID;

    }
    Product GetByID(int id)
    {
        foreach (Product? item in dataSource.Products) { if (item?.IsDeleted==false && item?.ID == id) return (Product)item; }
        throw new Exception("The product is not exist");
    }
    void Update(Product item)
    {
        Product? temp = dataSource.Products.Find(x => x?.ID == item.ID);
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The Product is not exist");
        if (temp?.IsDeleted == true)
            throw new Exception("The Product is deleted");
        Delete(item.ID);
        Add(item);
    }
    void Delete(int id)
    {
        Product? temp = dataSource.Products.Find(x => x.GetValueOrDefault().ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The product is not exist");
        if(temp?.IsDeleted==true)
            throw new Exception("The product is already deleted");
        dataSource.Products.Remove(temp);
        Product product=new Product { IsDeleted=true, Category=temp?.Category, InStock=temp?.InStock, Name=temp?.Name, Price=temp?.Price, ID= temp.GetValueOrDefault().ID};
        Add(product);
    }


    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<Product> GetAll()
    {
        List<Product> listGet = new List<Product>();
        foreach (Product? item in dataSource.Products) {if(item?.IsDeleted==false) listGet.Add((Product)item); }
        return listGet;
    }
}
