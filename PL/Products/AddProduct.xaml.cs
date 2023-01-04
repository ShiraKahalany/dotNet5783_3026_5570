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
using static System.Net.Mime.MediaTypeNames;
using BlApi;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private IBL bl = BLFactory.GetBL();
        string path;
        public AddProduct()
        {
            InitializeComponent();
            //ProductListView.ItemsSource = bl.Product.GetListedProducts();
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            SelectCategory.SelectedItem = BO.Category.All;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = InsertName.Text;
            int id= int.Parse(InserId.Text);
            double price = double.Parse(InsertPrice.Text);
            int amount =int.Parse(InsertAmount.Text);
            BO.Category category = (BO.Category)SelectCategory.SelectedItem;
            BO.Product newproduct = new BO.Product { ID = id, Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false };
            bl.Product.AddProduct(newproduct);
            MessageBox.Show("Seccessfully", "הוספת מוצר", MessageBoxButton.OK);
        Close();
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
