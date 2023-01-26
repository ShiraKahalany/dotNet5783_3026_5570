using BlApi;
using PO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace PL.Carts;

/// <summary>
/// Interaction logic for CustomerCart.xaml
/// </summary>
public partial class CustomerCart : Page
{
    private IBL bl = BLFactory.GetBL();
    PO.CartPO cartPO;
    Frame myframe;
    BO.Cart cartBo = new BO.Cart();
    public CustomerCart(BO.Cart cart, Frame frame)
    {
        InitializeComponent();
        cartBo = cart;
        cartBo.refreshCart();
        cartPO = PL.Tools.CopyBOCartToPO(cartBo);
        CartItems.ItemsSource = cartPO.Items;
        DataContext = cartPO;
        myframe = frame;
    }

    private void delete_Click(object sender, RoutedEventArgs e)
    {
        OrderItemPO or = ((OrderItemPO)((Button)sender).DataContext);
        int id = or?.ProductID ?? 0;
        try
        {
            bl.Cart.UpdateAmountOfProductInCart(cartBo, id, 0);
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Product Does Not Exist", "Not Exist", MessageBoxButton.OK);
        }
        catch (BO.NotInStockException)
        {
            MessageBox.Show("Sorry!It Is Out Of Stock", "ERROR", MessageBoxButton.OK);
        }
        cartPO.Items!.Remove(or!);
        cartPO.TotalPrice = Math.Round((double)(cartPO.TotalPrice - or?.Price * or?.Amount)!, 2);
    }

    private void chooseAmount_MouseEnter(object sender, MouseEventArgs e)
    {
        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        ((ComboBox)sender).ItemsSource = arr;
    }

    private void OrderConfirmation_Click(object sender, RoutedEventArgs e)=>  myframe.Content = new OrderConfirmationPage(cartBo, myframe);


    private void ContinueShopping_Click(object sender, RoutedEventArgs e)=> myframe.Content = new Products.CatalogPage("all", myframe, cartBo);


    private void UpdateAmount(object sender, int amount, bool isTextBox = false)
    {
        try
        {
            PO.OrderItemPO? item = new();
            BO.Product p = new();
            TextBox t = new();
            Button b = new();
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
                p = bl.Product.GetById(item?.ProductID ?? 0);
                bl.Cart.UpdateAmountOfProductInCart(cartBo, item?.ProductID ?? 0, amount);
                if (item!.Amount <= p.InStock)
                    item!.Amount = amount;
            }
            catch (BO.NotExistException)
            {
                MessageBox.Show("The Product Does Not Exist", "Not Exist", MessageBoxButton.OK);
            }
            catch (BO.NotInStockException)
            {
                MessageBox.Show("Sorry!It Is Out Of Stock", "ERROR", MessageBoxButton.OK);
                item!.Amount = p.InStock;
            }
            cartPO.TotalPrice = cartBo.TotalPrice;

            if (amount == 0)
            {
                cartPO?.Items?.Remove(item!);
                cartPO!.TotalPrice = cartBo.TotalPrice;
                return;
            }

            item.TotalItem = item!.Amount * item.Price;
        }
        catch (BO.NotInStockException)
        {
            MessageBox.Show("Not in stock" +
               "", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            return;
        }
    }

    private void Down_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int amount = ((PO.OrderItemPO)b.DataContext)?.Amount ?? 0;
        if (amount == 1)
            return;
        UpdateAmount(sender, amount - 1);
    }

    private void textAmount_TextChanged(object sender, TextChangedEventArgs e)
    {
        var t = (TextBox)sender;
        if (t.Text == "")
            return;

        int amount = int.Parse(t.Text);

        if (amount == 0)
            return;
        UpdateAmount(sender, amount, true);
    }
    private void OnlyNumbers(object sender, KeyEventArgs e) => Tools.EnterNumbersOnly(sender, e);
    private void Up_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        int amount = ((PO.OrderItemPO)b.DataContext)?.Amount ?? 0;
        UpdateAmount(sender, amount + 1);
    }
    private void EmptyCart_Click(object sender, RoutedEventArgs e)
    {
        cartPO?.Items?.Clear();
        cartBo?.Items?.Clear();
        cartBo!.TotalPrice = 0;
        cartPO!.TotalPrice = 0;
    }
    private void back_click(object sender, RoutedEventArgs e)=> myframe.Content = null;


}
