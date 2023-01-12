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
    public MainWindow()
    {
        InitializeComponent();
        ListCategories.Visibility = Visibility.Collapsed;
        cartDetails.Visibility = Visibility.Collapsed;
        pocart = new PO.CartPO { TotalPrice = 0, Items = new ObservableCollection<PO.OrderItemPO>() };
    BOcart = new BO.Cart { TotalPrice = 0, Items = new List<BO.OrderItem?>() };
    cartDetails.DataContext = pocart;
    }



    private void showCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Visible;
    }

    private void hideCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Hidden;
    }

    private void showCart(object sender, RoutedEventArgs e)
    {
        cartDetails.Visibility = Visibility.Visible;
    }

    private void hideCart(object sender, RoutedEventArgs e)
    {
        cartDetails.Visibility = Visibility.Hidden;
    }


    private void Categories_Click(object sender, RoutedEventArgs e)
    {
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
        cartDetails.Visibility = Visibility.Hidden;
        MainFrame.Content = new PL.Carts.CustomerCart(BOcart,MainFrame);
    }

    private void LogIn_Click(object sender, RoutedEventArgs e) => MainFrame.Content = new Manager.LogInManagerPage(MainFrame);

    private void LogoButton_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = null;
    }

    private void Tracking_Click(object sender, RoutedEventArgs e) => MainFrame.Content = new Orders.OrderTrackingByID(MainFrame);
    
}

