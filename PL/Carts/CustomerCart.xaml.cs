using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using PO;
namespace PL.Carts;

/// <summary>
/// Interaction logic for CustomerCart.xaml
/// </summary>
public partial class CustomerCart : Page
{
    private IBL bl = BLFactory.GetBL();
    int[] numArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 17, 18, 19 };
    public CustomerCart(PO.CartPO cartPO)
    {
        InitializeComponent();
        CartItems.ItemsSource = cartPO.Items;
        CartItems.DataContext=cartPO.Items;
        totalPrice.DataContext=cartPO.TotalPrice;
        NoItems.DataContext = (cartPO.Items!.Count==0);
        CartDetailsGrid.DataContext = (cartPO.Items!.Count != 0);
        //CartDetailsGrid.Visibility= Visibility.Collapsed;
        //chooseAmount.ItemSource = numArray;


    }

    private void delete_Click(object sender, RoutedEventArgs e)
    {

    }
}
