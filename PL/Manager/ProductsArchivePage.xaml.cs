using BlApi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
        ProductListView.ItemsSource = observeproducts;
    }

    private void Restore_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Are you sure you want to return the product to the store?", "Restore product", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.No)
            return;
        PO.ProductPO? restorepro = ((Button)(sender)).DataContext as PO.ProductPO;
        try
        {
            bl.Product.Restore(restorepro?.ID ?? 0);
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("Product Not Exist", "Not Exist Product", MessageBoxButton.OK);

        }
        observeproducts.Remove(restorepro!);
        MessageBox.Show("Seccessfully Restored", "Restore Product", MessageBoxButton.OK);
        observeproductsToSave.Add(restorepro!);
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void ShowDeletedProduct(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        new Products.DeletedProduct((PO.ProductPO)ProductListView.SelectedItem, observeproducts).ShowDialog();
        BOproducts = bl.Product.GetProducts(BO.Filters.filterByIsDeleted);
        observeproducts.Clear();
        observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
    }


}
