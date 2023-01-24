using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Products;

/// <summary>
/// Interaction logic for DeletedProduct.xaml
/// </summary>
public partial class DeletedProduct : Window
{
    private ObservableCollection<PO.ProductPO> observeproducts = new ObservableCollection<PO.ProductPO>();
    private IEnumerable<BO.Product> BOproducts;
    private IBL bl = BLFactory.GetBL();
    PO.ProductPO poProduct;

    public DeletedProduct(PO.ProductPO poPro, ObservableCollection<PO.ProductPO> products)
    {
        InitializeComponent();
        BOproducts = bl.Product.GetProducts(BO.Filters.filterByIsDeleted);
        observeproducts=products;
        poProduct = poPro;
        DataContext = poProduct;
        SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void Restore(object sender, RoutedEventArgs e)
    {
        PO.ProductPO? restorepro = ((Button)(sender)).DataContext as PO.ProductPO;
        try
        {
            bl.Product.Restore(restorepro!.ID);
            observeproducts.Remove(restorepro);
            Close();
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("Product Not Exist", "Not Exist Product", MessageBoxButton.OK);
        }       
    }
}
