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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Product_Manager_Window.xaml
    /// </summary>
    public partial class Product_Manager_Window : Window
    {
        public Product_Manager_Window()
        {
            InitializeComponent();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e) => Products_Manager.Content = new ProductsListManager();
        private void Button_Click_DeletedList(object sender, RoutedEventArgs e) => Products_Manager.Content = new DeletedProductsManager();
        private void Button_Click_Add(object sender, RoutedEventArgs e) => Products_Manager.Content = new AddNewProductManager();
    }
}
