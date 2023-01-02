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
    private ObservableCollection<BO.ProductForList> products = new ObservableCollection<BO.ProductForList>();
    Frame frame;
    private PO.CartPO pocart;
    public CatalogPage(string category, Frame mainFrame, PO.CartPO cart)
    {
        InitializeComponent();
        pocart = cart;
        switch(category)
        {
            case "kitchen":
                products = (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Kitchen)).ToObservable(products);
                break;
            case "living_room":
                products = (bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Living_room)).ToObservable(products);
                break;
            case "bath_room":
                products = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bathroom).ToObservable(products);
                break;
            case "bed_room":
                products = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Bedroom).ToObservable(products);
                break;
            case "garden":
                products = bl.Product.GetProductList(BO.Filters.filterByCategory, BO.Category.Garden).ToObservable(products);
                break;
            case "all":
                products = bl.Product.GetProductList().ToObservable(products);
                break;
        }
        listCatalog.DataContext = products;
        frame = mainFrame;
    }

    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        frame.Content = new ProductDetails((BO.ProductForList)listCatalog.SelectedItem, pocart);
    }
}
