﻿using BlApi;
using System;
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
namespace PL.Carts;

/// <summary>
/// Interaction logic for OrderConfirmationPage.xaml
/// </summary>
public partial class OrderConfirmationPage : Page
{
    private BO.Cart BOcart;
    private IBL bl = BLFactory.GetBL();
    Frame myframe;

    public OrderConfirmationPage(BO.Cart cart, Frame frame)
    {
        InitializeComponent();
        OrderSuccessGrid.Visibility = Visibility.Hidden;
        BOcart = cart;
        DataContext = BOcart;
        myframe = frame;
        ItemsListView.ItemsSource = BOcart.Items;
    }

    #region PlaceOrder_Click
    private void PlaceOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int id = bl.Cart.MakeAnOrder(BOcart);
            OrderId.DataContext = id;
            BOcart.Items?.Clear();
            BOcart.TotalPrice = 0;
            BOcart.CustomerName = null;
            BOcart.CustomerAddress = null;
            BOcart.CustomerEmail = null;
            OrderSuccessGrid.Visibility = Visibility.Visible;
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Order Not Exist", "Not Exist");
        }
        catch (BO.NoNameException)
        {
            MessageBox.Show("Name Is Missing. Please Try Again", "No Name");
        }
        catch (BO.NoAddressException)
        {
            MessageBox.Show("Address Is Missing. Please Try Again", "No Name");
        }
        catch (BO.IllegalEmailException)
        {
            MessageBox.Show("Illegal Email Address. Please Try Again", "ERROR");
        }
        catch (BO.NotItemsInCartException)
        {
            MessageBox.Show("There Are No Items In The Cart. Please Start Shopping", "No Items");
        }
        catch (BO.NotInStockException ex)
        {
            MessageBox.Show(ex.Message, "Not In Stock");
        }
        catch (BO.AmountNotPossitiveException)
        {
            MessageBox.Show("Amount Not Positive. Please Try Again", "Illegal Amount");
        }
        catch (BO.AlreadyExistException)
        {
            MessageBox.Show("Already Exist!", "ERROR", MessageBoxButton.OK);
        }

    }
    #endregion

    #region ContinueShopping_Click
    private void ContinueShopping_Click(object sender, RoutedEventArgs e)
    {
        myframe.Content = new Products.CatalogPage("all", myframe, BOcart);
    }
    #endregion
}
