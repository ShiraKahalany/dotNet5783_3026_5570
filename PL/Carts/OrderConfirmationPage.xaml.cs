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
        try
        {
            int id = bl.Cart.MakeAnOrder(BOcart);
            MessageBox.Show("Purchase Seccessfully :) Your Order ID Is " + id, "THANK YOU", MessageBoxButton.OK);
            BOcart.Items.Clear();
            BOcart.TotalPrice = 0;
            BOcart.CustomerName = null;
            BOcart.CustomerAddress = null;
            BOcart.CustomerEmail = null;
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exist", "Not Exist");
        }
        catch (BO.NoNameException)
        {
            MessageBox.Show("Name Is Missing. Please Try Again", "No Name");
        }
        catch (BO.NoAddressException)
        {
            MessageBox.Show("Address Is Missing. Please Try Again", "No Name");
        }
        catch (BO.IllegalEmailException)
        {
            MessageBox.Show("Illegal Email Address. Please Try Again", "ERROR");
        }
        catch (BO.NotItemsInCartException)
        {
            MessageBox.Show("There Are No Items In The Cart. Please Start Shopping", "No Items");
        }
        catch (BO.NotInStockException ex)
        {
            MessageBox.Show(ex.Message, "Not In Stock");
        }
        catch (BO.AmountNotPossitiveException)
        {
            MessageBox.Show("Amount Not Positive. Please Try Again", "Illegal Amount");
        }
        catch (BO.AlreadyExistException)
        {
            MessageBox.Show("Already Exist!", "ERROR", MessageBoxButton.OK);
        }

        

        //int id = bl.Cart.MakeAnOrder(BOcart);
        //MessageBox.Show("Purchase Seccessfully :)", "THANK YOU", MessageBoxButton.OK);
    }
}
