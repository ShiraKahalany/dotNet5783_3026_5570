using BlImplementation;
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
using BlApi;
namespace PL.Orders;

/// <summary>
/// Interaction logic for DeletedOrderPage.xaml
/// </summary>
public partial class DeletedOrderPage : Page
{
    Frame myFrame;
    private IBL bl = BLFactory.GetBL();
    BO.Order boOrder = new BO.Order();
    PO.OrderForListPO poorder = new();
    private PO.OrderPO order = new();
    private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
    private ObservableCollection<PO.OrderForListPO> observeproductsToSave = new ObservableCollection<PO.OrderForListPO>();

    public DeletedOrderPage(PO.OrderForListPO POorder, ObservableCollection<PO.OrderForListPO> obse, ObservableCollection<PO.OrderForListPO> toSave, Frame MainManagerOptionsFrame)
    {
        InitializeComponent();
        observeproductsToSave = toSave;
        ob = obse;
        poorder = POorder;
        int id = poorder.ID;
        myFrame = MainManagerOptionsFrame;
        try
        {
            boOrder = bl.Order.GetDeletedOrderById(id)!;
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
        ItemsListView.ItemsSource = boOrder.Items;
    }

    #region GoBack_click
    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
    #endregion

    #region Reorder_Click
    private void Reorder_Click(object sender, RoutedEventArgs e)
    {
        new Orders.CanceledOrderUpdatedDetailsWindow(poorder, ob, observeproductsToSave).ShowDialog();
        NavigationService.GoBack();
    }
    #endregion
}
