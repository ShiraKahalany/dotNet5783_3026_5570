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

namespace PL.Manager;

/// <summary>
/// Interaction logic for ManagerOptionsPage.xaml
/// </summary>
public partial class ManagerOptionsPage : Page
{
    public ManagerOptionsPage()
    {
        InitializeComponent();
    }

    #region Products Click
    private void Button_Click_Products(object sender, RoutedEventArgs e) => MainManagerOptionsFrame.Content = new ManagerProductsPage(MainManagerOptionsFrame);
    #endregion

    #region Orders Click
    private void Button_Click_Orders(object sender, RoutedEventArgs e) => MainManagerOptionsFrame.Content = new ManagerOrdersPage(MainManagerOptionsFrame);
    #endregion

    #region Track The Process Of Sending Orders Click
    private void TrackOrders_Button_Click(object sender, RoutedEventArgs e)=> new OrderTrackingWindow().Show();
    #endregion

}
