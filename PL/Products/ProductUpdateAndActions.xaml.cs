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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using BlApi;
namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductUpdateAndActions.xaml
    /// </summary>
    public partial class 
        
        
        
        
        ProductUpdateAndActions : Window
    {
        private IBL bl = BLFactory.GetBL();
        PO.ProductPO poProduct;
        BO.Category category;

        public ProductUpdateAndActions(PO.ProductPO poPro, ObservableCollection<PO.ProductPO> products)
        {
            InitializeComponent();
            poProduct = poPro;
            DataContext = poProduct;
            // BO.Product pro = bl.Product.GetProduct(poProduct.ID)!;
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //SelectCategory.SelectedItem = poProduct.Category;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string name = InsertName.Text;
           int id = int.Parse(InserId.Text);
            double price = double.Parse(InsertPrice.Text);
            int amount = int.Parse(InsertInStock.Text);
            BO.Category category = (BO.Category)SelectCategory.SelectedItem;
            BO.Product newproduct = new BO.Product { ID = id, Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false };
            bl.Product.UpdateProduct(newproduct);
            poProduct.ID = id;
            poProduct.Name = name;  
            poProduct.Price = price;
            poProduct.InStock = amount;
            poProduct.Category = category;
            MessageBox.Show("Seccessfully", "UPDATE PRODUCT", MessageBoxButton.OK);
            Close();
        }

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void InserId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
