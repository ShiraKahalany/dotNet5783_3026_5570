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
using PO;
using BlApi;
namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTrackingCustomer : Page
{
    BO.Order boOrder;
    private IBL bl = BLFactory.GetBL();
    public OrderTrackingCustomer(BO.Order order)
    {
        InitializeComponent();
        boOrder = order;
        DataContext = boOrder;
    }



   
}
