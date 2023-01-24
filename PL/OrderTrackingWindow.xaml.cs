using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PL;

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
   // public DateTime now { get; set; } = DateTime.Now;
   DateTime now = DateTime.Now;
    int addedDays = 0;
    bool toContinue = true;
    int progress = 0;
    int from = 0;

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
        DataContext = this;
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
        if (from==0)
            MessageBox.Show("yesssss it does!!!");
        else if(from==1)
        {
             toContinue = true;
            from = 0;
        }
    }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        while (toContinue)
        {
            now += day ;
            //now.now += day;
            //addedDays++;
            if (worker.WorkerReportsProgress)
                worker.ReportProgress(1);
            Thread.Sleep(1000);
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
            try
            {
                if (orders[i].Status == BO.OrderStatus.Ordered && orders[i].OrderDate <= now.AddDays(-31))
                    bl.Order.UpdateStatusToShipped(orders[i].ID/*, now*/);
                else if (orders[i].Status == BO.OrderStatus.Shipped && orders[i].ShipDate <= now.AddDays(-14))
                    bl.Order.UpdateStatusToProvided(orders[i].ID/*, now*/);
                if (orders[i].Status != BO.OrderStatus.Delivered)
                    toContinue = true;
            }
            catch (BO.OrderHasDeliveredException)
            { }
            catch (BO.OrderHasNotShippedException)
            { }
            catch(BO.OrderHasShippedException)
            { }
            catch(BO.NotExistException)
            { }
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
        //toContinue = true;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (worker.WorkerSupportsCancellation)
        {
            worker.CancelAsync();
            toContinue = false;
            from = 1;
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        BO.OrderTracking orderTracking = bl.Order.FollowOrder((((Button)(sender)).DataContext as PO.OrderPO)!.ID)!;
        new Orders.OrderWatchWindow(orderTracking).ShowDialog();
    }

}

