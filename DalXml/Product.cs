namespace Dal;
using DalApi;
using DO;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "products"; //Linq to XML

    

    static IEnumerable<XElement> createStudentElement(DO.Product product)
    {
        yield return new XElement("ID", product.ID);
        if (product.Name is not null)
            yield return new XElement("Name", product.Name);
        yield return new XElement("Category", product.Category);
        yield return new XElement("Price", product.Price);
        yield return new XElement("InStock", product.InStock);
        yield return new XElement("IsDeleted", product.IsDeleted);
        yield return new XElement("Path", product.Path);
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s)).Where(filter);

    //public DO.Product GetById(int id) =>
    //    (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
    //    .FirstOrDefault(st => st.ToIntNullable("ID") == id)
    //    // fix to: throw new DalMissingIdException(id);
    //    ?? throw new Exception("missing id"))!;

    public int Add(DO.Product product)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        if (product.ID>=100000 && XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == product.ID) is null)
        {
            productsRootElem.Add(new XElement("Product", createStudentElement(product)));
            XMLTools.SaveListToXMLElement(productsRootElem, s_products);
            return product.ID;
        }
        product.ID = XMLTools.RunningProductID();
        productsRootElem.Add(new XElement("Product", createStudentElement(product)));
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
        return product.ID;
    }

    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        XElement product = productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == id) ??throw new DO.NotExistException();
       if(product.ToBoolNullable("IsDeleted") ==true)
            throw new DO.NotExistException();
        //product.Remove();
        product.SetElementValue("IsDeleted", true);
        //productsRootElem.Add(product);
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }

    public void Update(DO.Product doProduct)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        //XElement x = productsRootElem.Elements().FirstOrDefault(p => (int?)p.Element("ID") == doProduct.ID);
        //if (x == null || (bool)x.Element("IsDeleted"))
        //    throw new DO.NotExistException();
        //x.Remove();

        (productsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == doProduct.ID /*&& st.ToBoolNullable("IsDeleted") == false*/)
            ?? throw new DO.NotExistException()).Remove();
        Add(doProduct);
    }

    public void DeletePermanently(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        (productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DO.NotExistException()).Remove();
    }

    public void Restore(DO.Product item)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        if(productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == item.ID && (bool)st.Element("IsDeleted") == false) != null)
            throw new DO.NotExistException();
        XElement product = productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == item.ID && (bool)st.Element("IsDeleted") == true);
        //if (product.ToBoolNullable("IsDeleted") == false)
        //    throw new DO.NotExistException();
        //DeletePermanently(item.ID);
        product.SetElementValue("IsDeleted", false);
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }

    public DO.Product? GetTByFilter(Func<DO.Product?, bool> filter)
    {
        DO.Product? pro = XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s)).Where(filter).FirstOrDefault();
        if(pro == null) 
            throw new DO.NotExistException();
        return pro;
    }

    static DO.Product? getProduct(XElement p) =>
        p.ToIntNullable("ID") is null ? null : new DO.Product()
        {
            ID = (int)p.Element("ID")!,
            Name = (string)p.Element("Name")!,
            Category = p.ToEnumNullable<DO.Category>("Category") ?? Category.All,
            Price = p.ToDoubleNullable("Price") ?? 0,
            InStock = p.ToIntNullable("InStock") ?? 0,
            IsDeleted = (bool)(p.Element("IsDeleted"))!,
            Path = (string)p.Element("Path")!
        };
}