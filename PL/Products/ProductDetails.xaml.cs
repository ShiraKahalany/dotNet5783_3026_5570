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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Page
    {
        private IBL bl = BLFactory.GetBL();
        private BO.ProductForList product = new();
        

        public ProductDetails(BO.ProductForList pro)
        {
            InitializeComponent();
            product = pro;
            DataContext = product;  
            
        }

        private void AddTocart_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart = new BO.Cart();
            int id;
            int amount;
            bl.Cart.AddProductToCart(cart, id, amount);
        }
    }
        
    }
}
