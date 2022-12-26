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

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerProductsPage.xaml
    /// </summary>
    public partial class ManagerProductsPage : Page
    {
        public ManagerProductsPage()
        {
            InitializeComponent();
            ManagerProductsFrame.Content = new ProductsListManager();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e) => new Products.AddProduct().Show();

        
    }
}
