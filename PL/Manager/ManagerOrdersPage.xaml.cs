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
        try
        {
            BOorderforlist = bl.Order.GetOrders()!;
        }
        catch (BO.NoItemsException)
        {
            MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
        }
        
        ob = BOorderforlist!.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
        OrderListView.ItemsSource = ob;
        AttributeSelector.SelectedItem = BO.OrderStatus.None;
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));

    }

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((BO.OrderStatus)AttributeSelector.SelectedItem == BO.OrderStatus.None)
        {
            try
            {
                BOorderforlist = bl.Order.GetOrders()!;
            }
            catch (BO.NoItemsException)
            {
                MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
            }
        }
        else
        {
            try
            {
                BOorderforlist = bl.Order.GetOrderList(BO.Filters.filterByStatus, (BO.OrderStatus)AttributeSelector.SelectedItem);
            }
            catch (BO.NotExistException)
            {
                MessageBox.Show("There Are No Orders", "No Orders", MessageBoxButton.OK);
            }
        }
        ob.Clear();
        ob = BOorderforlist!.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
    }

    private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        PO.OrderForListPO po=(PO.OrderForListPO)((ListView)sender).SelectedItem;
        BO.OrderStatus selectedStatus = (BO.OrderStatus)Enum.Parse(typeof(BO.OrderStatus), AttributeSelector.SelectedItem.ToString()!);
        myframe.Content = new Orders.OrderTracking(po, ob, selectedStatus);
    }

    private void DeleteOrder_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Are you sure you want to cancel the order?", "Delete order", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.No)
            return;
        PO.OrderForListPO? po = ((Button)(sender)).DataContext as PO.OrderForListPO;
        int id = po?.ID ?? 0;
        try
        {
            BO.Order or = bl.Order.GetOrderById(id)!;
            bl.Order.CancelOrder(or);
            ob.Remove(po!);
        }
        catch (BO.IllegalIdException)
        {
            MessageBox.Show("Illegal ID", "Can Not Cancal Order");

        }
        catch (BO.CanNotUpdateOrderException)
        {
            MessageBox.Show("The Order Has Already Been Shipped", "Can Not Cancal Order");

        }
        catch(BO.CantCancelOrderException)
        {
            MessageBox.Show("Can Not Cancal Order", "ERROR");
        }
        catch(BO.NotExistException)
        {
            MessageBox.Show("The Order Does Not Exist", "ERROR");
        }
        
    }

    private void ShowDeletedOrders_Click(object sender, RoutedEventArgs e) => myframe.Content = new Manager.OrdersArchivePage(ob, myframe);

}
