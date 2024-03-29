﻿using BlApi;
using PL.Products;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
    private IBL bl = BLFactory.GetBL();
    private PO.CartPO pocart;
    private BO.Cart BOcart;
    BO.Order order;
    public MainWindow()
    {
        InitializeComponent();
        ListCategories.Visibility = Visibility.Collapsed;
        pocart = new PO.CartPO { TotalPrice = 0, Items = new ObservableCollection<PO.OrderItemPO>() };
        BOcart = new BO.Cart { TotalPrice = 0, Items = new List<BO.OrderItem?>() };
    }

    #region showCategory
    private void showCategory(object sender, RoutedEventArgs e) => ListCategories.Visibility = Visibility.Visible;
    #endregion

    #region HideCategory
    private void hideCategory(object sender, RoutedEventArgs e) => ListCategories.Visibility = Visibility.Hidden;
    #endregion

    #region Categories_Click
    private void Categories_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        Tracking.IsEnabled = true;
        ListCategories.Visibility = Visibility.Visible;
    }
    #endregion

    #region ListCategories_Click
    private void ListCategories_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Content = new CatalogPage(((Button)sender).Name, MainFrame, BOcart);
        MainFrame.Visibility = Visibility.Visible;
    }
    #endregion

    #region CartButton_Click
    private void CartButton_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        Tracking.IsEnabled = true;
        MainFrame.Content = new PL.Carts.CustomerCart(BOcart, MainFrame);
    }
    #endregion

    #region LogIn_Click
    private void LogIn_Click(object sender, RoutedEventArgs e)
    {
        ((Button)(sender)).IsEnabled = false;
        Tracking.IsEnabled = true;
        MainFrame.Content = null;
    }
    #endregion

    #region LogoButton_Click
    private void LogoButton_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        Tracking.IsEnabled = true;
        MainFrame.Content = null;
    }
    #endregion

    #region OnlyNumbers
    private void OnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);
    #endregion

    #region Tracking_Click
    private void Tracking_Click(object sender, RoutedEventArgs e)
    {
        LogIn.IsEnabled = true;
        ((Button)(sender)).IsEnabled = false;
        MainFrame.Content = null;
    }
    #endregion

    #region CloseManagerLogIn_Click
    private void CloseManagerLogIn_Click(object sender, RoutedEventArgs e)
    {
        PasswordBox.Password = "";
        LogIn.IsEnabled = true;
    }
    #endregion

    #region CloseOrderTracking_Click
    private void CloseOrderTracking_Click(object sender, RoutedEventArgs e)
    {
        PasswordBox.Password = "";
        Tracking.IsEnabled = true;
    }
    #endregion

    #region ManagerlogInWithPassword_Click
    private void ManagerlogInWithPassword_Click(object sender, RoutedEventArgs e) => EnterPassword();
    #endregion

    #region OrderTrackingID_Click
    private void OrderTrackingID_Click(object sender, RoutedEventArgs e) => EnterID();
    #endregion

    #region EnterPressed_KeyDown
    private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) EnterPassword();
    }
    #endregion

    #region EnterPassword Func
    private void EnterPassword()
    {
        if (PasswordBox.Password == "1234")
        {
            LogIn.IsEnabled = true;
            PL.Manager.ManagerOptionsPage homeManager = new();
            PasswordBox.Password = "";
            MainFrame.Content = homeManager;
        }
    }
    #endregion

    #region EnterID Func
    private void EnterID()
    {
        int orderID = int.Parse(IDText.Text);
        try
        {
            order = bl.Order.GetOrderById(orderID)!;
            Tracking.IsEnabled = true;
            IDText.Text = "";
            MainFrame.Content = new Orders.OrderTrackingCustomer(order, MainFrame);
        }
        catch (BO.IllegalIdException)
        {
            MessageBox.Show("The ID number is not standard. Enter 4 digits", "OrderTracking", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.OrderNotExistException)
        {
            MessageBox.Show("Order not found", "OrderTracking", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    #endregion

}

