using PL.Products;
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

namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for ManagerOptionsWindow.xaml
    /// </summary>
    public partial class ManagerOptionsWindow : Window
    {
        public ManagerOptionsWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click_Products(object sender, RoutedEventArgs e) => new Product_Manager_Window().Show();

        private void Button_Click_Products(object sender, RoutedEventArgs e) => MainManagerFrame.Content = new ManagerProductsPage();
        private void Button_Click_Orders(object sender, RoutedEventArgs e) => MainManagerFrame.Content = new ManagerOrdersPage();


    }
}
