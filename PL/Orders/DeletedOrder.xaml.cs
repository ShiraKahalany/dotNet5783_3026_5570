using BlApi;
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
using System.Windows.Shapes;

namespace PL.Orders;

/// <summary>
/// Interaction logic for DeletedOrder.xaml
/// </summary>
public partial class DeletedOrder : Window
{
    BO.Order boOrder;
    private IBL bl = BLFactory.GetBL();
    public DeletedOrder(PO.OrderForListPO POorder, ObservableCollection<PO.OrderForListPO> obse)
    {
        InitializeComponent();
        DataContext = boOrder;
    }
}


