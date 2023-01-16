using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
namespace PL.Products
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private IBL bl = BLFactory.GetBL();
        string path;
        private ObservableCollection<PO.ProductPO> observeproducts = new ObservableCollection<PO.ProductPO>();
        private IEnumerable<BO.Product> BOproducts;
        public AddProduct(ObservableCollection<PO.ProductPO> ob)
        {
            InitializeComponent();
            observeproducts = ob;
            //ProductListView.ItemsSource = bl.Product.GetListedProducts();
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //SelectCategory.Items.Remove(BO.Category.All);
            // SelectCategory.SelectedItem = BO.Category.All;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = InsertName.Text;
           // int id = int.Parse(InserId.Text);
            double price = double.Parse(InsertPrice.Text);
            int amount = int.Parse(InsertAmount.Text);
            BO.Category category = (BO.Category)SelectCategory.SelectedItem;
            BO.Product newproduct = new BO.Product { ID = id, Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false, Path = path };
            try
            {
                bl.Product.AddProduct(newproduct);
                MessageBox.Show("Seccessfully", "Add Product", MessageBoxButton.OK);

                observeproducts.Add(PL.Tools.CopyProp<BO.Product, PO.ProductPO>(newproduct));
                Close();
            }
            catch(BO.WrongDetailsException)
            {
                MessageBox.Show("Your Details Are Wrong.Try Again", "Add Product", MessageBoxButton.OK);
            }
            catch(BO.AlreadyExistException)
            {
                MessageBox.Show("The Product Is Already Exist", "Can Not Add Product", MessageBoxButton.OK);
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
                path = (ProductImage.Source).ToString();
            }
        }

    }
}
