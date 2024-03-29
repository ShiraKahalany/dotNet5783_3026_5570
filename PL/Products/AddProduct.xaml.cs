﻿using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
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
        //private IEnumerable<BO.Product> BOproducts;

        public AddProduct(ObservableCollection<PO.ProductPO> ob)
        {
            InitializeComponent();
            observeproducts = ob;
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        #region AddPro_Click
        private void AddPro_Click(object sender, RoutedEventArgs e)
        {
            string name = InsertName.Text;
            double price = double.Parse(InsertPrice.Text);
            int amount = int.Parse(InsertAmount.Text);

            BO.Category category = (BO.Category)SelectCategory.SelectedItem;
            BO.Product newproduct = new BO.Product { Name = name, Price = price, InStock = amount, Category = category, IsDeleted = false, Path = path };
            try
            {
                int id = bl.Product.AddProduct(newproduct);
                newproduct.ID = id; 
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
        #endregion

        #region ChangeImageButton_Click
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
        #endregion

        #region OnlyNumbers
        private void OnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);
        #endregion

        #region EnterNumbersOrPointOnly
        private void EnterNumbersOrPointOnly(object sender, KeyEventArgs e) => Tools.EnterNumbersOrPointOnly(sender, e);
        #endregion
    }
}
