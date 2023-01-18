using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        private IBL bl = BLFactory.GetBL();
        private ObservableCollection<PO.OrderPO> ob = new ObservableCollection<PO.OrderPO>();
        IEnumerable<BO.Order> orders;
        private BackgroundWorker worker;
        TimeSpan day = new TimeSpan(24, 0, 0);
        DateTime now = DateTime.Now;

        public OrderTrackingWindow()
        {
            InitializeComponent();
            try
            {
                orders = bl.Order.GetOrdersByFilter();
            }
            catch (BO.NoItemsException)
            {
                MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
            }
            ob = orders.ToObservableByConverter<BO.Order, PO.OrderPO>(ob, PL.Tools.BoToPoOrder);
            OrderListView.ItemsSource = ob;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("yesssss it does!!!");
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            now += day * 0.3;
            if (worker.WorkerReportsProgress)
                worker.ReportProgress(1);
            Thread.Sleep(2000);
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            var ToShipped = from BO.Order order in orders
                            where order.OrderDate + day * 4 <= now && order.Status == BO.OrderStatus.Ordered
                            select bl.Order.UpdateStatusToShipped(order.ID);
            var ToDelivered = from BO.Order order in orders
                              where order.ShipDate + day * 4 <= now && order.Status == BO.OrderStatus.Shipped
                              select bl.Order.UpdateStatusToProvided(order.ID);
            ob = orders.ToObservableByConverter<BO.Order, PO.OrderPO>(ob, PL.Tools.BoToPoOrder);

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }

    }
}
