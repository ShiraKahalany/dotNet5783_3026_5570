using BlApi;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using PO;
namespace PL.Products;

/// <summary>
/// Interaction logic for CatalogPage.xaml
/// </summary>
public partial class CatalogPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.ProductItemPO> products = new ObservableCollection<PO.ProductItemPO>();
    private IEnumerable<BO.ProductItem> BOproducts;
    Frame frame;
    //private PO.CartPO pocart;
    private BO.Cart bocart;
    public CatalogPage(string category, Frame mainFrame, BO.Cart bcart)
    {
        InitializeComponent();
        //pocart = cart;
        bocart = bcart;
        switch (category)
        {
            case "kitchen":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Kitchen);
                break;
            case "living_room":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Living_room);
                //products = (bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Living_room)).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "bath_room":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Bathroom);
                //products = bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Bathroom).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "bed_room":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Bedroom);
                //products = bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Bedroom).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "garden":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Garden);
               // products = bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Garden).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "all":
                BOproducts = bl.Product.GetProductItemsList(bocart);
               // products = bl.Product.GetProductItemsList().ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
        }
        products = BOproducts.ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
        listCatalog.DataContext = products;
        frame = mainFrame;
    }
    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        //BO.ProductItem p = bl.Product.GetProductItem(((PO.ProductItemPO)listCatalog.SelectedItem).ID, bocart);
        frame.Content = new ProductDetails(((PO.ProductItemPO)listCatalog.SelectedItem), bocart, products, BOproducts);

    }

    //private void AddTocart_Click(object sender, RoutedEventArgs e)
    //{
    //    var b = (Button)sender;
    //    BO.Cart cart = new BO.Cart();
    //    int id = ((BO.ProductForList)b.DataContext).ID;
    //    bl.Cart.AddProductToCart(cart, id,1);
    //    MessageBox.Show("Add To Cart Seccessfully", "Add To Cart");



    //    //BO.Cart cart = new BO.Cart();
    //    //int id = product.ID;
    //    //int amount = (int)AmountOfProduct.SelectedItem;
    //    //double price = product.Price ?? 0;
    //    //bl.Cart.AddProductToCart(cart, id, amount);
    //    //pocart.Items.Add(new PO.OrderItemPO() { ProductID = id, Name = product.Name, Amount = amount, Price = price, IsDeleted = false, Path = product.Path });
    //    //pocart.TotalPrice += price * amount;
    //    //bl.Cart.AddProductToCart(cart, id, amount);
    //    //MessageBox.Show("Add To Cart Seccessfully", "Add To Cart", MessageBoxButton.OK);
    //    ////this.NavigationService.GoBack();

    //}

    //private void AddToCartFromCatalog(object sender, RoutedEventArgs e)
    //{
    //    BO.ProductForList p = ((Button)(sender)).DataContext as BO.ProductForList;
    //    int id = p?.ID??0;
    //    BO.Cart cart = new BO.Cart();
    //    bl.Cart.AddProductToCart(cart, id);
    //    double price = p?.Price ?? 0;
    //    pocart.AddToPOCart(new PO.OrderItemPO() { ProductID = id, Name = p?.Name, Amount = 1, Price = price, IsDeleted = false, Path = p?.Path });
    //}

    private void DeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        PO.ProductPO po = ((Button)(sender)).DataContext as PO.ProductPO;
        int id = po?.ID ?? 0;
        try
        {
            bl.Product.DeleteProduct(id);
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Product Does Not Exist", "Delete Product", MessageBoxButton.OK);
        }
        catch (BO.InAnOrderException)
        {
            MessageBox.Show("The Product Is In An Order", "Can Not Delete Product", MessageBoxButton.OK);
        }
        //observeproducts.Remove(po);
    }

}
