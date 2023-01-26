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
        ObservableCollection<PO.ProductPO> ob;
        string path;
        public ProductUpdateAndActions(PO.ProductPO poPro, ObservableCollection<PO.ProductPO> products)
        {
            InitializeComponent();
            poProduct = poPro;
            DataContext = poProduct;
            ob=products;
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            path = poProduct.Path!;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string name = InsertName.Text;
           int id = int.Parse(InserId.Text);
            double price = double.Parse(InsertPrice.Text);
            int amount = int.Parse(InsertInStock.Text);
            BO.Category category = (BO.Category)SelectCategory.SelectedItem;
            BO.Product newproduct = new BO.Product { ID = id, Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false, Path=path };
            try
            {
                bl.Product.UpdateProduct(newproduct);
                poProduct.ID = id;
                poProduct.Name = name;
                poProduct.Price = price;
                poProduct.InStock = amount;
                poProduct.Category = category;
                poProduct.Path = path;
                Close();
            }
            catch(BO.NotExistException)
            {
                MessageBox.Show("The Product Does Not Exist", "ERROR", MessageBoxButton.OK);
            }

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
                string[] str = (ProductImage.Source).ToString().Split("PL",2,StringSplitOptions.RemoveEmptyEntries);
                path = str[1];
            }
        }

        private void OnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);
        private void EnterNumbersOrPointOnly(object sender, KeyEventArgs e) => Tools.EnterNumbersOrPointOnly(sender, e);
    }
}
