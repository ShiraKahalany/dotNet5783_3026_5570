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
using System.Windows.Shapes;
using BlApi;
using System.Collections.ObjectModel;
using PO;

namespace PL.Orders;

/// <summary>
/// Interaction logic for UpdateItemInOrder.xaml
/// </summary>
public partial class UpdateItemInOrder : Window
{
    PO.OrderItemPO ItemPO;
    ObservableCollection<PO.OrderItemPO> itemsList;
    ObservableCollection<PO.OrderForListPO> ordersList;
    PO.OrderPO order;
    public UpdateItemInOrder(PO.OrderItemPO poItem, ObservableCollection<PO.OrderItemPO> items, ObservableCollection<PO.OrderForListPO> orders, PO.OrderPO or)
    {
        InitializeComponent();
        ItemPO= poItem;
        itemsList= items;
        ordersList= orders;
        order= or;
    }
}
