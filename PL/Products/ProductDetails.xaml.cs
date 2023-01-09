using BlApi;
using PO;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductDetails.xaml
/// </summary>
public partial class ProductDetails : Page
{
    private IBL bl = BLFactory.GetBL();
   // private BO.ProductItem BoProduct;
    private PO.ProductItemPO PoProduct;
    private PO.CartPO pocart;

    public ProductDetails(PO.ProductItemPO pro, PO.CartPO cart)
    {
        InitializeComponent();
        pocart= cart;
        PoProduct = pro;
        DataContext = PoProduct;
        int[] numArray = new int[20];
        for (int i = 0; i < 20; i++)
            numArray[i] = i+1;
        AmountOfProduct.ItemsSource = numArray;
        AmountOfProduct.SelectedItem = 1;
      //  BoProduct = PL.Tools.CopyProp<PO.ProductItemPO, BO.ProductItem>(PoProduct);
    }

    private void AddTocart_Click(object sender, RoutedEventArgs e)
    {
        BO.Cart cart = new BO.Cart();
        int id = PoProduct.ID;
        int amount = (int)AmountOfProduct.SelectedItem;
        double price = PoProduct.Price??0 ;
        bl.Cart.AddProductToCart(cart, id, amount);
        pocart.Items.Add(new PO.OrderItemPO() { ProductID = id,Name= PoProduct.Name, Amount = amount, Price = price, IsDeleted=false, Path= PoProduct.Path});
        pocart.TotalPrice += price * amount;
        bl.Cart.AddProductToCart(cart,id, amount);
        MessageBox.Show("Add To Cart Seccessfully", "Add To Cart", MessageBoxButton.OK);
        //this.NavigationService.GoBack();
    }

    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}



