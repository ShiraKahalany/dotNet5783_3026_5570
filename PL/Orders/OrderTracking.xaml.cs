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
using PO;
using BlApi;
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
        poorder=order.CopyFields<BO.Order,PO.OrderPO>(poorder);
        DataContext = poorder;
    }

   

    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (poorder.Status == BO.OrderStatus.Shipped)
            {
                boOrder=bl.Order.UpdateStatusToProvided(boOrder.ID);
                poorder = boOrder.CopyFields<BO.Order, PO.OrderPO>(poorder);
            }
        }
        catch(BO.OrderHasDeliveredException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BO.OrderHasNotShippedException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            boOrder=bl.Order.UpdateStatusToShipped(boOrder.ID);
            poorder = boOrder.CopyFields<BO.Order, PO.OrderPO>(poorder);
        }
        catch(BO.OrderHasShippedException)
        {
            MessageBox.Show("The Order Has Already Been Shipped", "Has Already Shipped", MessageBoxButton.OK);
        }
    }

}
