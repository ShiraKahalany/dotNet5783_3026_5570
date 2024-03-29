﻿using BlApi;
using BO;
using MaterialDesignThemes.Wpf;
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


namespace PL.Manager;

/// <summary>
/// Interaction logic for OrdersArchivePage.xaml
/// </summary>
public partial class OrdersArchivePage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
    private ObservableCollection<PO.OrderForListPO> observeproductsToSave = new ObservableCollection<PO.OrderForListPO>();
    private IEnumerable<BO.OrderForList> BOorders;
    Frame myframe;


    public OrdersArchivePage(ObservableCollection<PO.OrderForListPO> observeproducts, Frame MainManagerOptionsFrame)
    {
        InitializeComponent();
        observeproductsToSave = observeproducts;
        myframe = MainManagerOptionsFrame;
        try
        {
            BOorders = bl.Order.GetDeletedOrders()!;
            ob = BOorders!.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
            ProductListView.ItemsSource = ob;
        }
        catch(BO.NotExistException)
        {
            MessageBox.Show("There Are No Deleted Orders", "No Deleted", MessageBoxButton.OK);
        }
        catch(BO.NoItemsException)
        {
            MessageBox.Show("There Are No Deleted Orders", "No Deleted", MessageBoxButton.OK);
        }
    }

    #region RestoreOrder_Click
    private void RestoreOrder_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderForListPO? POor = ((Button)(sender)).DataContext as PO.OrderForListPO;
        new PL.Orders.CanceledOrderUpdatedDetailsWindow(POor!, ob,observeproductsToSave).ShowDialog();
    }
    #endregion

    #region GoBack_Click
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
    #endregion

    #region ProductListView_MouseDoubleClick
    private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        PO.OrderForListPO po = (PO.OrderForListPO)((ListView)sender).SelectedItem;
        myframe.Content = new Orders.DeletedOrderPage(po, ob, observeproductsToSave, myframe);
    }
    #endregion
}
