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
using PL.Products;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBL bl= BLFactory.GetBL();
    public MainWindow()
    {
        InitializeComponent();
        ListCategories.Visibility = Visibility.Collapsed;
        PO.CartPO pocart =new PO.CartPO();
       // kitchen.Content = BO.Category.Kitchen;
       // bed_room.DataContext = BO.Category.Bedroom;
       //living_room.DataContext = BO.Category.Living_room;
       // bath_room.DataContext = BO.Category.Bathroom;
       // garden.DataContext = BO.Category.Garden;
       // all.DataContext = BO.Category.All;
    }

    private void showProducts_Click(object sender, RoutedEventArgs e) => new ProductListWindow().ShowDialog();

    private void showCategory(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Visible;
    //    backgroundimage.Source = new BitmapImage(new Uri(@"/PL/back ground blurry.jpg", UriKind.Relative));
        
    }
    private void hideCategory(object sender,RoutedEventArgs e)
    {
        ListCategories.Visibility = Visibility.Hidden;
    }

    private void Categories_Click(object sender, RoutedEventArgs e)
    {
        ListCategories.Visibility= Visibility.Visible;
        MainFrame.Visibility = Visibility.Hidden;
        
    }
    //public void ListCategories_Click(object sender, RoutedEventArgs e) => new ProductListWindow().ShowDialog();

    private void ListCategories_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new CatalogPage(((Button)sender).Name, MainFrame);
        MainFrame.Visibility = Visibility.Visible;    
    }

    public void ListViewItem_Selected(object sender, RoutedEventArgs e)
    {

    }

    private void CartButton_Click(object sender, RoutedEventArgs e)
    {
       // Catalog.Content = new PL.Carts.CustomerCart();
    }

    private void LogIn_Click(object sender, RoutedEventArgs e) => MainFrame.Content= new Manager.LogInManagerPage(MainFrame);



}
