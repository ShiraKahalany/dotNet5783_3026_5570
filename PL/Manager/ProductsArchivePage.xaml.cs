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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using System.Collections.ObjectModel;
using PL.Products;

namespace PL.Manager;

/// <summary>
/// Interaction logic for ProductsArchivePage.xaml
/// </summary>
public partial class ProductsArchivePage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.ProductPO> observeproducts = new ObservableCollection<PO.ProductPO>();
    private ObservableCollection<PO.ProductPO> observeproductsToSave = new ObservableCollection<PO.ProductPO>();
    private IEnumerable<BO.Product> BOproducts;
    public ProductsArchivePage(ObservableCollection<PO.ProductPO> ob)
    {
        InitializeComponent();
        observeproductsToSave = ob;
        BOproducts = bl.Product.GetProducts(BO.Filters.filterByIsDeleted);
        observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
        //ProductListView.DataContext = observeproducts;
        ProductListView.ItemsSource = observeproducts;
    }

    private void Restore_Click(object sender, RoutedEventArgs e)
    {
       PO.ProductPO restorepro = ((Button)(sender)).DataContext as PO.ProductPO;
        bl.Product.Restore(restorepro.ID);
        observeproducts.Remove(restorepro);
        MessageBox.Show("Seccessfully Restored", "Restore Product", MessageBoxButton.OK);
        observeproductsToSave.Add(restorepro);
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}
