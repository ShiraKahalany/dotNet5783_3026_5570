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
using System.Windows.Shapes;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for CanceledOrderUpdatedDetailsWindow.xaml
    /// </summary>
    public partial class CanceledOrderUpdatedDetailsWindow : Window
    {

        private IBL bl = BLFactory.GetBL();
        BO.Order boOrder = new BO.Order();
        PO.OrderForListPO poorder = new();
        double totalPrice;        
        private PO.OrderPO order = new();
        private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
        private ObservableCollection<PO.OrderForListPO> observeproductsToSave = new ObservableCollection<PO.OrderForListPO>();
        public CanceledOrderUpdatedDetailsWindow(PO.OrderForListPO order, ObservableCollection<PO.OrderForListPO> obse, ObservableCollection<PO.OrderForListPO> toSave)
        {
            
            InitializeComponent();
            poorder = order;
            ob=obse;
            observeproductsToSave=toSave;
            notSuccess.Visibility = Visibility.Hidden;
           
            try
            {
                totalPrice=bl.Order.Restore(poorder.ID);
                thePrice.Text = totalPrice.ToString();
                ob.Remove(poorder);
                BO.Order or=bl.Order.GetOrderById(poorder.ID)!;
                poorder = or.CopyFields(poorder);
                observeproductsToSave.Add(poorder);

            }
            catch (BO.NotExistException)
            {
                success.Visibility = Visibility.Hidden;
                total.Visibility = Visibility.Hidden;
                price.Visibility = Visibility.Hidden;
                notSuccess.Visibility = Visibility.Visible;
            }
            catch (BO.NotInStockException)
            {
                success.Visibility = Visibility.Hidden;
                total.Visibility = Visibility.Hidden;
                price.Visibility = Visibility.Hidden;
                notSuccess.Visibility = Visibility.Visible;
            }
        }

        #region OK_Click
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
