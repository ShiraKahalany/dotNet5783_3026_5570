using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using PL.Products;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    private IBL bl = BLFactory.GetBL();
    private PO.CartPO pocart;
    private BO.Cart BOcart;
    BO.Order order;
    public MainWindow()
    {
        InitializeComponent();
        ListCategories.Visibility = Visibility.Collapsed;
        //cartDetails.Visibility = Visibility.Collapsed;
        pocart = new PO.CartPO { TotalPrice = 0, Items = new ObservableCollection<PO.OrderItemPO>() };
    BOcart = new BO.Cart { TotalPrice = 0, Items = new List<BO.OrderItem?>() };
   // cartDetails.DataContext = pocart;
    }

     

    private void showCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Visible;
    }

    private void hideCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Hidden;
    }

    //private void showCart(object sender, RoutedEventArgs e)
    //{
    //    cartDetails.Visibility = Visibility.Visible;
    //}

    private void hideCart(object sender, RoutedEventArgs e)
    {
        //cartDetails.Visibility = Visibility.Hidden;
    }


    private void Categories_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        Tracking.IsEnabled = true;
        ListCategories.Visibility = Visibility.Visible;
        //MainFrame.Visibility = Visibility.Hidden;

    }
    //public void ListCategories_Click(object sender, RoutedEventArgs e) => new ProductListWindow().ShowDialog();

    private void ListCategories_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new CatalogPage(((Button)sender).Name, MainFrame, BOcart);
        MainFrame.Visibility = Visibility.Visible;
    }

    public void ListViewItem_Selected(object sender, RoutedEventArgs e)
    {

    }

    private void CartButton_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        Tracking.IsEnabled = true;
        MainFrame.Content = new PL.Carts.CustomerCart(BOcart,MainFrame);
    }

    private void LogIn_Click(object sender, RoutedEventArgs e)
    {
       ((Button)(sender)).IsEnabled = false;
        Tracking.IsEnabled = true;
        MainFrame.Content = null;
        
    }

    private void MainFrame_Navigated(object sender, NavigationEventArgs e)
    {
      
    }

    private void LogoButton_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        Tracking.IsEnabled = true;
        MainFrame.Content = null;
    }

    private void OnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);
    private void Tracking_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        ((Button)(sender)).IsEnabled = false;
        MainFrame.Content = null;
    }

   
    private void CloseManagerLogIn_Click(object sender, RoutedEventArgs e)
    {
        //managerButton.Visibility = Visibility.Visible;
        //managerButton.IsEnabled = true;
        PasswordBox.Password = "";
        LogIn.IsEnabled = true;
    }
    
    private void CloseOrderTracking_Click(object sender, RoutedEventArgs e)
    {
        //managerButton.Visibility = Visibility.Visible;
        //managerButton.IsEnabled = true;
        PasswordBox.Password = "";
        Tracking.IsEnabled = true;
    }

    private void ManagerLogIn_Click(object sender, RoutedEventArgs e)
    {
        //managerButton.Visibility = Visibility.Hidden;
        //managerButton.IsEnabled = false;

    }

    private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
    {
        //if (e.LeftButton == MouseButtonState.Pressed)
        //{
        //    DragMove();
        //}
    }

    // private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

    private void ManagerlogInWithPassword_Click(object sender, RoutedEventArgs e)
    {
        EnterPassword();

    }
    
    private void OrderTrackingID_Click(object sender, RoutedEventArgs e)
    {
        EnterID();

    }

    private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) EnterPassword();
    }

    private void EnterPassword()
    {
        if (PasswordBox.Password == "1234")
        {
            LogIn.IsEnabled = true;
            PL.Manager.ManagerOptionsPage homeManager = new();
            PasswordBox.Password = "";
            MainFrame.Content = homeManager;
        }
    }

    private void EnterID()
    {
        int orderID = int.Parse(IDText.Text);
        try
        {
            order = bl.Order.GetOrderById(orderID)!;
            Tracking.IsEnabled = true;
            IDText.Text = "";
            MainFrame.Content = new Orders.OrderTrackingCustomer(order,MainFrame);
        }
        catch (BO.IllegalIdException)
        {
            MessageBox.Show("The ID number is not standard. Enter 4 digits", "OrderTracking", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.OrderNotExistException)
        {
            MessageBox.Show("Order not found", "OrderTracking", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    }

