﻿using BlApi;
using System.Collections.ObjectModel;
using System.Windows;

namespace PL.Orders;

/// <summary>
/// Interaction logic for UpdateItemInOrder.xaml
/// </summary>
public partial class UpdateItemInOrder : Window
{
    private IBL bl = BLFactory.GetBL();
    PO.OrderItemPO ItemPO;
    PO.OrderForListPO poorder = new();
    ObservableCollection<PO.OrderItemPO> itemsList;
    ObservableCollection<PO.OrderForListPO> ordersList;
    PO.OrderPO order;
    public UpdateItemInOrder(PO.OrderItemPO poItem, ObservableCollection<PO.OrderItemPO> items, ObservableCollection<PO.OrderForListPO> orders, PO.OrderPO or, PO.OrderForListPO po)
    {
        InitializeComponent();
        ItemPO = poItem;
        itemsList = items;
        ordersList = orders;
        order = or;
        poorder = po;
        DataContext = ItemPO;
        int[] numArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        comboBox.ItemsSource = numArray;
        comboBox.SelectedItem = 1;
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        //int amount = (int)(comboBox.SelectedItem);
        //try
        //{
        //    BO.Order newOrder= bl.Order.UpdateAmountOfProduct(order.ID, ItemPO.ProductID ?? 0, amount)!;
        //    //ordersList.Remove(poorder);
        //    order = newOrder.CopyFields(order);
        //    itemsList.Remove(ItemPO);
        //    ItemPO.Amount= amount;
        //    ItemPO.TotalItem=amount* ItemPO.Price;
        //   // ordersList.Remove(poorder);
        //    itemsList.Add(ItemPO);
        //    Close();
        //}
        //catch(BO.NotInStockException)
        //{
        //    MessageBox.Show("The product is not exist in this amount, can`t update");
        //}
        //catch (BO.NotExistException)
        //{
        //    MessageBox.Show("The product is not exist");
        //}
    }
}
