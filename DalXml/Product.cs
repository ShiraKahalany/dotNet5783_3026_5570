﻿namespace Dal;
using DalApi;
using DO;
using System.Linq;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "products"; //Linq to XML

    #region createProductElement
    static IEnumerable<XElement> createProductElement(DO.Product product)
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
    #endregion

    #region Get All Products
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s)).Where(filter);
    #endregion

    #region Get Product By ID
    public DO.Product GetById(int id) =>
        (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id && ((bool)st.Element("IsDeleted")!) == false)
        // fix to: throw new DalMissingIdException(id);
        ?? throw new DO.NotExistException("missing id"))!;
    #endregion

    #region Add
    public int Add(DO.Product product)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        if (product.ID >= 100000 && XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == product.ID) is null)
        {
            productsRootElem.Add(new XElement("Product", createProductElement(product)));
            XMLTools.SaveListToXMLElement(productsRootElem, s_products);
            return product.ID;
        }
        product.ID = XMLTools.RunningProductID();
        productsRootElem.Add(new XElement("Product", createProductElement(product)));
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
        return product.ID;
    }
    #endregion

    #region Delete
    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        XElement product = productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DO.NotExistException();
        if ((bool)product.Element("IsDeleted")! == true)
            throw new DO.NotExistException();
        //product.Remove();
        product.SetElementValue("IsDeleted", true);
        //productsRootElem.Add(product);
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }

    public void Update(DO.Product doProduct)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        (productsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("ID") == doProduct.ID && (bool)st.Element("IsDeleted")! == false)
            ?? throw new DO.NotExistException()).Remove();
        productsRootElem.Add(new XElement("Product", createProductElement(doProduct)));
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }
    #endregion

    #region Delete Permanently
    public void DeletePermanently(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        (productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new DO.NotExistException()).Remove();
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }
    #endregion

    #region Restore
    public void Restore(DO.Product item)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        if (productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == item.ID && (bool)st.Element("IsDeleted")! == false) != null)
            throw new DO.NotExistException();
        XElement product = productsRootElem.Elements().FirstOrDefault(st => (int?)st.Element("ID") == item.ID && (bool)st.Element("IsDeleted")! == true)!;
        //if (product.ToBoolNullable("IsDeleted") == false)
        //    throw new DO.NotExistException();
        //DeletePermanently(item.ID);
        product.SetElementValue("IsDeleted", false);
        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }
    #endregion

    #region Get Product By Filter
    public DO.Product? GetTByFilter(Func<DO.Product?, bool> filter)
    {
        DO.Product? pro = XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s)).Where(filter).FirstOrDefault();
        if (pro == null)
            throw new DO.NotExistException();
        return pro;
    }
    #endregion

    #region getProduct From XElement
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
    #endregion
}