using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
namespace PL.Products;

/// <summary>
/// Interaction logic for CatalogPage.xaml
/// </summary>
public partial class CatalogPage : Page
{
    private IBL bl = BLFactory.GetBL();
    //private ObservableCollection<BO.ProductForList> products = new ObservableCollection<BO.ProductForList>();
    Frame frame;
    public CatalogPage(string category, Frame mainFrame)
    {
        InitializeComponent();
        switch(category)
        {
            case "kitchen":
                listCatalog.DataContext = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Kitchen);
                break;
            case "living_room":
                listCatalog.DataContext = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Living_room);
                break;
            case "bath_room":
                listCatalog.DataContext = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bathroom);
                break;
            case "bed_room":
                listCatalog.DataContext =bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bedroom);
                break;
            case "garden":
                listCatalog.DataContext = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Garden);
                break;
            case "all":
                listCatalog.DataContext = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.All);
                break;
        }

        frame = mainFrame;
    }

    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
     //   frame.Content = new ProductDetails((BO.ProductForList)listCatalog.SelectedItem, cart);
    }
}
