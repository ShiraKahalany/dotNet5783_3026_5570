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
    /// Interaction logic for Products_Manager.xaml
    /// </summary>
    public partial class Products_Manager : Window
    {
        public Products_Manager()
        {
            InitializeComponent();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e)=> Products_Manager.Content = new ProductsListManager();
        private void Button_Click_DeletedList(object sender, RoutedEventArgs e) => Products_Manager.Content = new DeletedProductsManager();
        private void Button_Click_Add(object sender, RoutedEventArgs e) => Products_Manager.Content = new AddNewProductManager();


    }
}
