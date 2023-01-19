using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        private IBL bl = BLFactory.GetBL();
        private ObservableCollection<PO.OrderPO> ob = new ObservableCollection<PO.OrderPO>();
        List<BO.Order> orders;
        private BackgroundWorker worker;
        TimeSpan day = new TimeSpan(24, 0, 0);
        DateTime now = DateTime.Now;
        bool toContinue = true;

        public OrderTrackingWindow()
        {
            InitializeComponent();
            try
            {
                orders = bl.Order.GetOrdersByFilter().ToList();
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
            if(!toContinue)
            MessageBox.Show("yesssss it does!!!");
            else
            {
                MessageBox.Show("In Middle!!!");
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (toContinue)
            {
                now += day * 0.1;
                if (worker.WorkerReportsProgress)
                    worker.ReportProgress(1);
                Thread.Sleep(4000);
            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            //foreach (BO.Order order in orders)
            //    if (order.OrderDate /*+ day * 4*/ <= now && order.Status == BO.OrderStatus.Ordered)
            //        bl.Order.UpdateStatusToShipped(order.ID);

            //foreach (BO.Order order in orders)
            //    if (order.ShipDate /*+ day * 4*/ <= now && order.Status == BO.OrderStatus.Shipped)
            //        bl.Order.UpdateStatusToProvided(order.ID);

            toContinue = false;
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Status == BO.OrderStatus.Ordered && orders[i].OrderDate <= now + day * 4)
                    bl.Order.UpdateStatusToShipped(orders[i].ID);
                else if (orders[i].Status == BO.OrderStatus.Shipped && orders[i].ShipDate <= now + day * 4)
                    bl.Order.UpdateStatusToProvided(orders[i].ID);
                if (orders[i].Status != BO.OrderStatus.Delivered)
                    toContinue = true;
              // Thread.Sleep(500);
            }

            //var ToShipped = from BO.Order order in orders
            //                where order.OrderDate /*+ day * 4*/ <= now && order.Status == BO.OrderStatus.Ordered
            //                select order;

            try
            {
                orders = bl.Order.GetOrdersByFilter().ToList();
            }
            catch (BO.NoItemsException)
            {
                MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
            }
            int a = 2;

            //var ToDelivered = from BO.Order order in orders
            //                  where order.ShipDate /*+ day * 4*/ <= now && order.Status == BO.OrderStatus.Shipped
            //                  select  (bl.Order.UpdateStatusToProvided(order.ID));

            ob = orders.ToObservableByConverter<BO.Order, PO.OrderPO>(ob, PL.Tools.BoToPoOrder);

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation)
            {
                worker.CancelAsync();
                toContinue = false;
            }
            MessageBox.Show("I am Stoppped");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BO.OrderTracking orderTracking = bl.Order.FollowOrder((((Button)(sender)).DataContext as PO.OrderPO)!.ID)!;
            new Orders.OrderWatchWindow(orderTracking).ShowDialog();
        }

    }
}
