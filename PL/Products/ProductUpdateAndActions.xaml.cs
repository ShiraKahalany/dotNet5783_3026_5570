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
namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductUpdateAndActions.xaml
    /// </summary>
    public partial class ProductUpdateAndActions : Window
    {
        private IBL bl = BLFactory.GetBL();
        public ProductUpdateAndActions(BO.ProductForList prol)
        {
            BO.Product pro =bl.Product.GetProduct(prol.ID)!;
            InitializeComponent();
            InserId.Text = pro.ID.ToString();
            InsertName.Text = pro.Name;
            InsertPrice.Text = pro.Price.ToString();
            InsertAmount.Text=pro.InStock.ToString();
            //SelectCategory.Text = pro.Category.ToString();
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            SelectCategory.SelectedItem = prol.Category;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = InsertName.Text;
           int id = int.Parse(InserId.Text);
            double price = double.Parse(InsertPrice.Text);
            int amount = int.Parse(InsertAmount.Text);
            BO.Category category = (BO.Category)SelectCategory.SelectedItem;
            BO.Product newproduct = new BO.Product { ID = id, Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false };
            bl.Product.UpdateProduct(newproduct);
            MessageBox.Show("Seccessfully", "עידכון מוצר", MessageBoxButton.OK);
            Close();
        }

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(InserId.Text);
            bl.Product.DeleteProduct(id);
            MessageBox.Show("Seccessfully", "מחיקת מוצר", MessageBoxButton.OK);
            Close();
        }
    }
}
