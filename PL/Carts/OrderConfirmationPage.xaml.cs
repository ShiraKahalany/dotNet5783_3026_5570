using BlApi;
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
namespace PL.Carts;

/// <summary>
/// Interaction logic for OrderConfirmationPage.xaml
/// </summary>
public partial class OrderConfirmationPage : Page
{
    private BO.Cart BOcart;
    private IBL bl = BLFactory.GetBL();

    public OrderConfirmationPage(BO.Cart cart)
    {
        InitializeComponent();
        BOcart= cart;
        DataContext= BOcart;
    }

    private void PlaceOrder_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Purchase Seccessfully :)", "THANK YOU", MessageBoxButton.OK);
    }
}
