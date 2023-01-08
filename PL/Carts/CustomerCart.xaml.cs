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
using BlApi;
using PO;
namespace PL.Carts;

/// <summary>
/// Interaction logic for CustomerCart.xaml
/// </summary>
public partial class CustomerCart : Page
{
    private IBL bl = BLFactory.GetBL();
    PO.CartPO cartpo;
    Frame myframe;
    public CustomerCart(PO.CartPO cartPO,Frame frame)
    {
        InitializeComponent();
        cartpo = cartPO;
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
        BO.Cart boCart = PL.Tools.CopyPOCartToBO(cartpo);
        bl.Cart.UpdateAmountOfProductInCart(boCart, id, 0);
        cartpo.Items!.Remove(or);
        cartpo.TotalPrice=Math.Round((double)(cartpo.TotalPrice-or.Price*or.Amount)!,2);
    }

    private void chooseAmount_MouseEnter(object sender, MouseEventArgs e)
    {
        int[] arr = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
       ((ComboBox)sender).ItemsSource = arr;
    }

    private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
    {
        myframe.Content = new OrderConfirmationPage();
    }

    private void ContinueShopping_Click(object sender, RoutedEventArgs e)
    {
        myframe.Content = new Products.CatalogPage("all", myframe, cartpo);
    }
}
