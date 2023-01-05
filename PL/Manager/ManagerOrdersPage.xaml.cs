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
using System.Collections.ObjectModel;
using BlApi;
using PL.Orders;
namespace PL;

/// <summary>
/// Interaction logic for ManagerOrdersPage.xaml
/// </summary>
public partial class ManagerOrdersPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
    private IEnumerable<BO.OrderForList> BOorderforlist;
    Frame myframe;
    public ManagerOrdersPage(Frame MainManagerOptionsFrame)
    {
        InitializeComponent();
        myframe = MainManagerOptionsFrame;
        BOorderforlist = bl.Order.GetOrders();
        ob = BOorderforlist.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
        //ProductListView.DataContext = observeproducts;
        ProductListView.ItemsSource = ob;
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));

    }

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((BO.OrderStatus)AttributeSelector.SelectedItem == BO.OrderStatus.None)
            BOorderforlist = bl.Order.GetOrders();
        else
            BOorderforlist = bl.Order.GetOrderList(BO.Filters.filterByStatus, (BO.OrderStatus)AttributeSelector.SelectedItem);
        ob.Clear();
        ob = BOorderforlist.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
        // ProductListView.DataContext = observeproducts;
    }

    private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        //new ProductUpdateAndActions((PO.ProductPO)ProductListView.SelectedItem, observeproducts).ShowDialog();
        //if ((BO.Category)AttributeSelector.SelectedItem != BO.Category.All)
        //{
        //    BOproducts = bl.Product.GetProducts(BO.Filters.filterByCategory, (BO.Category)AttributeSelector.SelectedItem).ToList();
        //    observeproducts.Clear();
        //    observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
        //}
    }

    private void DeleteOrder_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderForListPO po = ((Button)(sender)).DataContext as PO.OrderForListPO;
        int id = po?.ID ?? 0;
        bl.Order.CancelOrder(id);
        ob.Remove(po);
    }

    private void ShowDeletedOrders_Click(object sender, RoutedEventArgs e)
    {
       // myframe.Content = new Manager.OrdersArchivePage(ob);
    }
}
