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
    private IBL bl = BLFactory.GetBL();
    BO.Cart bocart;
    public OrderConfirmationPage()
    {
        InitializeComponent();
    }

    private void PlaceOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.Cart.MakeAnOrder(bocart);
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exist", "Not Exist", MessageBoxButton.OK);
        }
        catch (BO.NoNameException)
        {
            MessageBox.Show("Name Is Missing. Please Try Again", "No Name", MessageBoxButton.OK);
        }
        catch (BO.NoAddressException)
        {
            MessageBox.Show("Address Is Missing. Please Try Again", "No Name", MessageBoxButton.OK);
        }
        catch (BO.IllegalEmailException)
        {
            MessageBox.Show("Illegal Email Address. Please Try Again", "ERROR", MessageBoxButton.OK);
        }
        catch (BO.NotItemsInCartException)
        {
            MessageBox.Show("There Are No Items In The Cart. Please Start Shopping", "No Items", MessageBoxButton.OK);
        }
        catch (BO.NotInStockException ex)
        {
            MessageBox.Show(ex.Message, "Not In Stock", MessageBoxButton.OK);
        }
        catch (BO.AmountNotPossitiveException)
        {
            MessageBox.Show("Amount Not Positive. Please Try Again", "Illegal Amount", MessageBoxButton.OK);
        }
        catch (BO.AlreadyExistException)
        {
            MessageBox.Show("Already Exist!", "ERROR", MessageBoxButton.OK);
        }

        MessageBox.Show("Purchase Seccessfully :)", "THANK YOU", MessageBoxButton.OK); 
    }
}
