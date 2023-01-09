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
namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Page
{
    BO.Order boOrder;
    public OrderTracking(BO.Order order)
    {
        InitializeComponent();
        boOrder = order;
        PO.OrderPO poorder=new();
        poorder=order.CopyFields<BO.Order,PO.OrderPO>(poorder);
        DataContext = poorder; 
    }

   

    private void UpdateDel_Click(object sender, RoutedEventArgs e)
    {

    }

    private void UpdateShip_Click(object sender, RoutedEventArgs e)
    {

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
