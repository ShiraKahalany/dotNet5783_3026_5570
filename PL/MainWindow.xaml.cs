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

namespace PL
{
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
        }

        private void showProducts_Click(object sender, RoutedEventArgs e) => new ProductListWindow().ShowDialog();

        private void Connection_Click(object sender, RoutedEventArgs e)=>new ConnectionWindow().ShowDialog();
        private void showCategory(object sender, RoutedEventArgs e)
        {
            ListCategories.Visibility = Visibility.Visible;
        }
        private void hideCategory(object sender,RoutedEventArgs e)
        {
            ListCategories.Visibility = Visibility.Hidden;
        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            ListCategories.Visibility= Visibility.Visible;
        }
        public void ListCategories_Click(object sender, RoutedEventArgs e) => new ProductListWindow().ShowDialog();


    }
}
