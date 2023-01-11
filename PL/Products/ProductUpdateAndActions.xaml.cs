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
        string path;
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
            BO.Product newproduct = new BO.Product { ID = id, Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false,Path=path };
            try
            {
                bl.Product.UpdateProduct(newproduct);
            }
            catch(BO.NotExistException)
            {
                MessageBox.Show("The Product Does Not Exist", "ERROR", MessageBoxButton.OK);
            }
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

        private void changeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            //f.Filter = "All Files| *.*";
            f.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                ProductImage.Source = new BitmapImage(new Uri(f.FileName));
                path = (ProductImage.Source).ToString();
            }
        }

    }
}
