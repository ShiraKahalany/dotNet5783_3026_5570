using BlApi;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace PL.Products;

/// <summary>
/// Interaction logic for CatalogPage.xaml
/// </summary>
public partial class CatalogPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<BO.ProductForList> products = new ObservableCollection<BO.ProductForList>();
    Frame frame;
    private PO.CartPO pocart;
    public CatalogPage(string category, Frame mainFrame, PO.CartPO cart)
    {
        InitializeComponent();
        pocart = cart;
        switch (category)
        {
            case "kitchen":
                products = (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Kitchen)).ToObservable(products);
                break;
            case "living_room":
                products = (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Living_room)).ToObservable(products);
                break;
            case "bath_room":
                products = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bathroom).ToObservable(products);
                break;
            case "bed_room":
                products = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bedroom).ToObservable(products);
                break;
            case "garden":
                products = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Garden).ToObservable(products);
                break;
            case "all":
                products = bl.Product.GetProductList().ToObservable(products);
                break;
        }
        listCatalog.DataContext = products;
        frame = mainFrame;
    }
    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        frame.Content = new ProductDetails((BO.ProductForList)listCatalog.SelectedItem, pocart);
    }

    private void AddTocart_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        BO.Cart cart = new BO.Cart();
        int id = ((BO.ProductForList)b.DataContext).ID;
        bl.Cart.AddProductToCart(cart, id,1);
        MessageBox.Show("Add To Cart Seccessfully", "Add To Cart");



        //BO.Cart cart = new BO.Cart();
        //int id = product.ID;
        //int amount = (int)AmountOfProduct.SelectedItem;
        //double price = product.Price ?? 0;
        //bl.Cart.AddProductToCart(cart, id, amount);
        //pocart.Items.Add(new PO.OrderItemPO() { ProductID = id, Name = product.Name, Amount = amount, Price = price, IsDeleted = false, Path = product.Path });
        //pocart.TotalPrice += price * amount;
        //bl.Cart.AddProductToCart(cart, id, amount);
        //MessageBox.Show("Add To Cart Seccessfully", "Add To Cart", MessageBoxButton.OK);
        ////this.NavigationService.GoBack();

    }

}
