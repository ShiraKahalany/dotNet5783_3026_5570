using BlApi;
using BO;
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


namespace PL.Manager
{
    /// <summary>
    /// Interaction logic for OrdersArchivePage.xaml
    /// </summary>
    public partial class OrdersArchivePage : Page
    {
        private IBL bl = BLFactory.GetBL();
        private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
        private ObservableCollection<PO.OrderForListPO> observeproductsToSave = new ObservableCollection<PO.OrderForListPO>();
        private IEnumerable<BO.OrderForList> BOorders;

        public OrdersArchivePage(ObservableCollection<PO.OrderForListPO> observeproducts)
        {
            InitializeComponent();
            observeproductsToSave = observeproducts;
            try
            {
                BOorders = bl.Order.GetDeletedOrders();
            }
            catch(BO.NotExistException)
            {
                MessageBox.Show("There Are No Deleted Orders", "No Deleted", MessageBoxButton.OK);
            }
            catch(BO.NoItemsException)
            {
                MessageBox.Show("There Are No Deleted Orders", "No Deleted", MessageBoxButton.OK);
            }
            ob = BOorders.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
            ProductListView.ItemsSource = ob;
        }


        private void RestoreOrder_Click(object sender, RoutedEventArgs e)
        {
            PO.OrderForListPO POor = ((Button)(sender)).DataContext as PO.OrderForListPO;
            try
            {
                bl.Order.Restore(POor.ID);
                ob.Remove(POor);
                MessageBox.Show("Seccessfully Restored", "Restore Order", MessageBoxButton.OK);
                observeproductsToSave.Add(POor);
            }
            catch(BO.NotExistException)
            {
                MessageBox.Show("There Are No Deleted Orders", "No Deleted", MessageBoxButton.OK);
            }
           
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //new Orders.DeletedOrder((PO.ProductPO)ProductListView.SelectedItem, observeproducts).ShowDialog();
            //BOproducts = bl.Product.GetProducts(BO.Filters.filterByIsDeleted);
            //observeproducts.Clear();
            //observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);

        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
