﻿using System;
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
using System.Collections.ObjectModel;
using BlApi;
using PL.Orders;
namespace PL;

/// <summary>
/// Interaction logic for ManagerOrdersPage.xaml
/// </summary>
public partial class ManagerOrdersPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.OrderForListPO> ob = new ObservableCollection<PO.OrderForListPO>();
    private IEnumerable<BO.OrderForList> BOorderforlist;
    Frame myframe;
    public ManagerOrdersPage(Frame MainManagerOptionsFrame)
    {
        InitializeComponent();
        myframe = MainManagerOptionsFrame;
        try
        {
            BOorderforlist = bl.Order.GetOrders();
        }
        catch (BO.NoItemsException)
        {
            MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
        }
        
        ob = BOorderforlist.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
        //ProductListView.DataContext = observeproducts;
        OrderListView.ItemsSource = ob;
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));

    }

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((BO.OrderStatus)AttributeSelector.SelectedItem == BO.OrderStatus.None)
        {
            try
            {
                BOorderforlist = bl.Order.GetOrders();
            }
            catch (BO.NoItemsException)
            {
                MessageBox.Show("There Are NO Items", "ERROR", MessageBoxButton.OK);
            }
        }
        else
        {
            try
            {
                BOorderforlist = bl.Order.GetOrderList(BO.Filters.filterByStatus, (BO.OrderStatus)AttributeSelector.SelectedItem);
            }
            catch (BO.NotExistException)
            {
                MessageBox.Show("There Are No Orders", "No Orders", MessageBoxButton.OK);
            }
        }
        ob.Clear();
        ob = BOorderforlist.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
        // ProductListView.DataContext = observeproducts;
    }

    private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        PO.OrderForListPO po=(PO.OrderForListPO)((ListView)sender).SelectedItem;
        //BO.OrderForList boorder = new();
        //boorder = po.CopyFields<PO.OrderForListPO,BO.OrderForList>(boorder);
        int id = po.ID;
        BO.Order boorder = new BO.Order();
        try
        {
           boorder = bl.Order.GetOrderById(id)!;
        }
        catch (BO.IllegalIdException)
        {
            MessageBox.Show("Illegal Order ID", "ERROR");
        }
        catch(BO.OrderNotExistException)
        {
            MessageBox.Show("Order Not Exist", "ERROR");
        }
        myframe.Content = new Orders.OrderTracking(boorder);
      //myframe.Content = new Orders.OrderTracking(boorder, po);

        if ((AttributeSelector.SelectedItem != null) && (BO.OrderStatus)AttributeSelector.SelectedItem != BO.OrderStatus.None)
        {
            BOorderforlist = bl.Order.GetOrderList(BO.Filters.filterByStatus, (BO.OrderStatus)AttributeSelector.SelectedItem).ToList();
            ob.Clear();
            //ob = BOorderforlist.ToObservableByConverter<BO.>;
            ob = BOorderforlist.ToObservableByConverter<BO.OrderForList, PO.OrderForListPO>(ob, PL.Tools.CopyProp<BO.OrderForList, PO.OrderForListPO>);
        }


        //if ((BO.Category)AttributeSelector.SelectedItem != BO.Category.All)
        //{
        //    BOproducts = bl.Product.GetProducts(BO.Filters.filterByCategory, (BO.Category)AttributeSelector.SelectedItem).ToList();
        //    observeproducts.Clear();
        //    observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
        //}
    }

    private void DeleteOrder_Click(object sender, RoutedEventArgs e)
    {
        PO.OrderForListPO po = ((Button)(sender)).DataContext as PO.OrderForListPO;
        int id = po?.ID ?? 0;
        try
        {
            bl.Order.CancelOrder(id);
        }
        catch(BO.CanNotUpdateOrderException)
        {
            MessageBox.Show("The Order Has Already Been Shipped", "Can Not Cancal Order");

        }
        catch(BO.CantCancelOrderException)
        {
            MessageBox.Show("Can Not Cancal Order", "ERROR");
        }
        catch(BO.NotExistException)
        {
            MessageBox.Show("The Order Does Not Exist", "ERROR");
        }
        ob.Remove(po);
    }

    private void ShowDeletedOrders_Click(object sender, RoutedEventArgs e)
    {
       myframe.Content = new Manager.OrdersArchivePage(ob);
    }
}
