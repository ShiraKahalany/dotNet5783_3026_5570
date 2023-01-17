using BlApi;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Page
{
    private IBL bl = BLFactory.GetBL();
    BO.Order boOrder= new BO.Order();
    PO.OrderForListPO poorder = new();
    private PO.OrderPO order = new();
    private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
    public OrderTracking(PO.OrderForListPO POorder, ObservableCollection<PO.OrderForListPO> obse)
    {
        InitializeComponent();
        ob = obse;
        poorder = POorder;
        int id = poorder.ID;
       // BO.Order boOrder = new BO.Order();
        try
        {
            boOrder = bl.Order.GetOrderById(id)!;
        }
        catch (BO.IllegalIdException)
        {
            MessageBox.Show("Illegal Order ID", "ERROR");
        }
        catch (BO.OrderNotExistException)
        {
            MessageBox.Show("Order Not Exist", "ERROR");
        }
        order = boOrder.CopyFields<BO.Order, PO.OrderPO>(order);
        DataContext = order;
        //CartItems.ItemsSource = boOrder.Items;
        //CartItems.DataContext = boOrder.Items;
    }


    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {
        bool isStatusChanged = true;
        try
        {
            boOrder = bl.Order.UpdateStatusToProvided(boOrder.ID);
        }
        catch (BO.OrderHasDeliveredException)
        {
            MessageBox.Show("The Order Has Already Delivered", "Has Already Delivered", MessageBoxButton.OK);
            isStatusChanged = false;
        }
        catch (BO.OrderHasNotShippedException)
        {
            MessageBox.Show("The Order Has Not Yet Shipped", "Not Shipped Yet", MessageBoxButton.OK);
            isStatusChanged = false;
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exist", "Not Exist", MessageBoxButton.OK);
            isStatusChanged = false;
        }
        poorder = boOrder.CopyFields<BO.Order, PO.OrderForListPO>(poorder);
        order= boOrder.CopyFields<BO.Order, PO.OrderPO>(order);
        if (isStatusChanged)
            ob.Remove(poorder);
        // }
    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
        bool isStatusChanged = true;
        try
        {
            boOrder = bl.Order.UpdateStatusToShipped(boOrder.ID);
           // poorder.ShipDate = boOrder.ShipDate;
            poorder = boOrder.CopyFields<BO.Order, PO.OrderForListPO>(poorder);
           // DataContext=poorder;
        }
        catch (BO.OrderHasShippedException)
        {
            MessageBox.Show("The Order Has Already Been Shipped", "Has Already Shipped");
            isStatusChanged = false;
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exit", "Not Exist");
            isStatusChanged = false;
        }
        poorder = boOrder.CopyFields<BO.Order, PO.OrderForListPO>(poorder);
        order = boOrder.CopyFields<BO.Order, PO.OrderPO>(order);
        if (isStatusChanged)
            ob.Remove(poorder);
    }

    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}
