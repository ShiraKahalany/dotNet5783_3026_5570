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
using BlApi;
using PL.Products;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBL bl = BLFactory.GetBL();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            AttributeSelector.SelectedItem = BO.Category.All;
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((BO.Category)AttributeSelector.SelectedItem == BO.Category.All)
                ProductListView.ItemsSource = bl.Product.GetListedProducts();
            else
                ProductListView.ItemsSource = bl.Product.GetProductList(BO.Filters.filterByCategory, (BO.Category)AttributeSelector.SelectedItem);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            new AddProduct().Show();
            ProductListView.ItemsSource = bl.Product.GetListedProducts();
            AttributeSelector.SelectedItem = BO.Category.All;
        }

        //private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e) => new ProductUpdateAndActions((BO.ProductForList)ProductListView.SelectedItem).Show();
        
    }
}
