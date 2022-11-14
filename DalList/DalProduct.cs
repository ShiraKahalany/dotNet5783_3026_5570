
using DalApi;
using DO;
namespace Dal;
//namespace DalApi;

public class DalProduct:IProduct
{
    DataSource dataSource = new DataSource();
    int Add(Product item)
    {
        Product? temp = dataSource.Products.Find(x => x.GetValueOrDefault().ID == item.ID;
        if (temp == null)
            throw new Exception("The product already exists");
        dataSource.AddProduct(item);
        return item.ID;
    }
    Product GetByID(int id)
    {
        foreach (Product item in dataSource.Products) { if (item.ID == id) return item; }
        throw new Exception("The product is not exist");
    }
    void Update(Product item)
    {
        Delete(item.ID);
        Add(item);
    }
    void Delete(int id)
    {
        Product? temp = dataSource.Products.Find(x => x.GetValueOrDefault().ID == id); //check if the element exist in the orders list
        if (temp == null) //if it is not exist throw exception
            throw new Exception("The product is not exist");
        temp.GetValueOrDefault().IsDeleted = true; //update the isdeleted field of the order
    }


    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<Product> GetAll()
    {
        List<Product> listGet = new List<Product>();
        foreach (Product item in dataSource.Products) { listGet.Add(item); }
        return listGet;
    }
}
