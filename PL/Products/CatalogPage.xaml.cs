using BlApi;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using PO;
namespace PL.Products;

/// <summary>
/// Interaction logic for CatalogPage.xaml
/// </summary>
public partial class CatalogPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.ProductItemPO> products = new ObservableCollection<PO.ProductItemPO>();
    Frame frame;
    private PO.CartPO pocart;
    public CatalogPage(string category, Frame mainFrame, PO.CartPO cart)
    {
        InitializeComponent();
        pocart = cart;
        switch (category)
        {
            case "kitchen":
                products = (bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Kitchen)).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "living_room":
                products = (bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Living_room)).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "bath_room":
                products = bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Bathroom).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "bed_room":
                products = bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Bedroom).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "garden":
                products = bl.Product.GetProductItemsList(BO.Filters.filterByCategory, BO.Category.Garden).ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
            case "all":
                products = bl.Product.GetProductItemsList().ToObservableByConverter(products, PL.Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>);
                break;
        }
        listCatalog.DataContext = products;
        frame = mainFrame;
    }
    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        frame.Content = new ProductDetails((PO.ProductItemPO)listCatalog.SelectedItem, pocart);
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
        bl.Product.DeleteProduct(id);
        //observeproducts.Remove(po);
    }

}
