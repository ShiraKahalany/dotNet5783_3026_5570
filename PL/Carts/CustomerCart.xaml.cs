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
using BlApi;
using BO;
using PO;
namespace PL.Carts;

/// <summary>
/// Interaction logic for CustomerCart.xaml
/// </summary>
public partial class CustomerCart : Page
{
    private IBL bl = BLFactory.GetBL();
    PO.CartPO pocart;
    Frame myframe;
    BO.Cart cartBo = new BO.Cart();
    public CustomerCart(BO.Cart cart,Frame frame)
    {
        InitializeComponent();
        cartBo = cart;
        pocart = PL.Tools.CopyBOCartToPO(cartBo);
        //cartBo=cartPO.CopyPOCartToBO();
        CartItems.ItemsSource = cartPO.Items;
        CartItems.DataContext=cartPO.Items;
        totalPrice.DataContext=cartPO;
        NoItems.DataContext = cartPO;
        CartDetailsGrid.DataContext = cartPO;
        myframe=frame;
        //NoItems.DataContext = (cartPO.Items!.Count==0);
        //CartDetailsGrid.DataContext = (cartPO.Items!.Count != 0);
        //CartDetailsGrid.Visibility= Visibility.Collapsed;
        //chooseAmount.ItemSource = numArray;


    }

    private void delete_Click(object sender, RoutedEventArgs e)
    {
        OrderItemPO or = ((OrderItemPO)((Button)sender).DataContext);
        int id = or?.ProductID??0;
        BO.Cart boCart = PL.Tools.CopyPOCartToBO(pocart);
        try
        {
            bl.Cart.UpdateAmountOfProductInCart(boCart, id, 0);
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Product Does Not Exist", "Not Exist", MessageBoxButton.OK);
        }
        catch (BO.NotInStockException)
        {
            MessageBox.Show("Sorry!It Is Out Of Stock", "ERROR", MessageBoxButton.OK);
        }
        pocart.Items!.Remove(or);
        pocart.TotalPrice=Math.Round((double)(pocart.TotalPrice-or.Price*or.Amount)!,2);
    }

    private void chooseAmount_MouseEnter(object sender, MouseEventArgs e)
    {
        int[] arr = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
       ((ComboBox)sender).ItemsSource = arr;
    }

    private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
    {
        myframe.Content = new OrderConfirmationPage(cartBo);
    }

    private void ContinueShopping_Click(object sender, RoutedEventArgs e)
    {
        myframe.Content = new Products.CatalogPage("all", myframe, cartBo);
    }

    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private void UpdateAmount(object sender, int amount, bool isTextBox = false)
    {
        try
        {
            PO.OrderItemPO item = new();
            TextBox t;
            Button b;
            if (isTextBox)
            {
                t = (TextBox)sender;
                item = (PO.OrderItemPO)t.DataContext;
            }
            else
            {
                b = (Button)sender;
                item = (PO.OrderItemPO)b.DataContext;
            }
            try
            {
                bl.Cart.UpdateAmountOfProductInCart(cartBo, item?.ProductID ?? 0, amount);
            }
            catch (BO.NotExistException)
            {
                MessageBox.Show("The Product Does Not Exist", "Not Exist", MessageBoxButton.OK);
            }
            catch (BO.NotInStockException)
            {
                MessageBox.Show("Sorry!It Is Out Of Stock", "ERROR", MessageBoxButton.OK);
            }
        item = pocart.Items.FirstOrDefault(x => x.ID == item.ID);
            pocart.TotalPrice = cartBo.TotalPrice;
            if (amount == 0)
            {
                pocart.Items.Remove(item);
                pocart.TotalPrice = cartBo.TotalPrice;
                return;
            }
            item.Amount = amount;
        }
        catch (BO.NotInStockException ex)
        {
            MessageBox.Show("אין עוד מהמוצר הזה במלאי" +
               "", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
    }

    private void cmdDown_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int amount = ((PO.OrderItemPO)b.DataContext)?.Amount ?? 0;
        if (amount == 1)
            return;
        UpdateAmount(sender, amount - 1);
    }

    private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
    {
        var t = (TextBox)sender;
        int amount = int.Parse(t.Text);
        if (amount == 0)
            return;
        UpdateAmount(sender, amount, true);
    }

    private void cmdUp_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int amount = ((PO.OrderItemPO)b.DataContext)?.Amount ?? 0;
        UpdateAmount(sender, amount + 1);
    }
}
