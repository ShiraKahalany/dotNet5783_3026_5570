using BlApi;
using System.Windows;
using System.Windows.Controls;
namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Page
{
    private IBL bl = BLFactory.GetBL();
    BO.Order boOrder;
    PO.OrderPO poorder = new();
    public OrderTracking(BO.Order order)
    {
        InitializeComponent();
        boOrder = order;
        poorder = order.CopyFields<BO.Order, PO.OrderPO>(poorder);
        DataContext = poorder;
        //CartItems.ItemsSource = boOrder.Items;
        //CartItems.DataContext = boOrder.Items;
    }



    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {

        //if (poorder.Status == BO.OrderStatus.Shipped)
        // {
        try
        {
            boOrder = bl.Order.UpdateStatusToProvided(boOrder.ID);
        }
        catch (BO.OrderHasDeliveredException)
        {
            MessageBox.Show("The Order Has Already Delivered", "Has Already Delivered", MessageBoxButton.OK);
        }
        catch (BO.OrderHasNotShippedException)
        {
            MessageBox.Show("The Order Has Not Yet Shipped", "Not Shipped Yet", MessageBoxButton.OK);
        }
        catch(BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exist", "Not Exist", MessageBoxButton.OK);
        }
        poorder = boOrder.CopyFields<BO.Order, PO.OrderPO>(poorder);
           // }
    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            boOrder = bl.Order.UpdateStatusToShipped(boOrder.ID);
           // poorder.ShipDate = boOrder.ShipDate;
            poorder = boOrder.CopyFields<BO.Order, PO.OrderPO>(poorder);
           // DataContext=poorder;
        }
        catch (BO.OrderHasShippedException)
        {
            MessageBox.Show("The Order Has Already Been Shipped", "Has Already Shipped");
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exit", "Not Exist");
        }

        

    }

    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}
