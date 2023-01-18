using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        private IBL bl = BLFactory.GetBL();
        private IEnumerable<BO.Order> orders;
        BackgroundWorker worker;
        TimeSpan daytime =new TimeSpan(24,0,0);
        
        public OrderTrackingWindow()
        {
            InitializeComponent();
            try
            {
                orders = bl.Order.get;
            }
            catch (BO.NoItemsException)
            {
                MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
            }

            worker=new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            //מביא את כל ההזמנות שעבר X זמן מאז שנשלחו
            orders = bl.Order.

        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
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
