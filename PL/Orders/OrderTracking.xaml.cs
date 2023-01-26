using BlApi;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Navigation;
using BO;
using PO;

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Page
{
    private IBL bl = BLFactory.GetBL();
    ObservableCollection<PO.OrderItemPO> poItems = new();
    BO.Order boOrder= new BO.Order();
    PO.OrderForListPO poorder = new();
    private PO.OrderPO order = new();
    private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
    BO.OrderStatus stat;
    public OrderTracking(PO.OrderForListPO POorder, ObservableCollection<PO.OrderForListPO> obse, BO.OrderStatus status)
    {
        InitializeComponent();
        ob = obse;
        poorder = POorder;
        int id = poorder.ID;
        stat = status;
        try
        {
            boOrder = bl.Order.GetOrder(x=>x?.ID==id);
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
        poItems = boOrder.Items!.ToObservableByConverter<BO.OrderItem, PO.OrderItemPO>(poItems, PL.Tools.CopyProp<BO.OrderItem, PO.OrderItemPO>);
        DataContext = order;
        //ItemsListView.ItemsSource = boOrder.Items;
        ItemsListView.ItemsSource = poItems;
    }


    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {
        bool isStatusChanged = true;
        try
        {
            boOrder = bl.Order.UpdateStatusToProvided(boOrder.ID)!;
            
        }
        catch (BO.OrderHasDeliveredException)
        {
            MessageBox.Show("The Order Has Already Delivered", "Has Already Delivered", MessageBoxButton.OK);
            isStatusChanged = false;
        }
        catch (BO.OrderHasNotShippedException)
        {
            MessageBox.Show("The Order Has NOT Yet Shipped", "Not Shipped Yet", MessageBoxButton.OK);
            isStatusChanged = false;
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exist", "Not Exist", MessageBoxButton.OK);
            isStatusChanged = false;
        }
        poorder = boOrder.CopyFields<BO.Order, PO.OrderForListPO>(poorder);
        order = boOrder.CopyFields<BO.Order, PO.OrderPO>(order);
        if (isStatusChanged && stat!= BO.OrderStatus.None)
            ob.Remove(poorder);
        // }
    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
        bool isStatusChanged = true;
        try
        {
            boOrder = bl.Order.UpdateStatusToShipped(boOrder.ID)!;
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
        if (isStatusChanged &&(stat != BO.OrderStatus.None))
            ob.Remove(poorder);
    }

    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void updateItem(object sender, RoutedEventArgs e)
    {
        if(order.Status != BO.OrderStatus.Ordered)
        {
            MessageBox.Show("The order has already been shipped, no changes can be made");
            return;
        }
        PO.OrderItemPO? poitem = ((Button)(sender)).DataContext as PO.OrderItemPO;
        new UpdateItemInOrder(poitem, poItems, ob, order, poorder).ShowDialog();
    }
}
