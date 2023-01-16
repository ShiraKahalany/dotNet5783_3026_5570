﻿using BlApi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductDetails.xaml
/// </summary>
public partial class ProductDetails : Page
{
    private IBL bl = BLFactory.GetBL();
    private BO.ProductItem BoProduct = new BO.ProductItem();
    private IEnumerable<BO.ProductItem> BOproducts;
    private PO.ProductItemPO PoProduct;
    private BO.Cart bocart;
    private ObservableCollection<PO.ProductItemPO> products;

    public ProductDetails(PO.ProductItemPO pro, BO.Cart cart, ObservableCollection<PO.ProductItemPO> prod, IEnumerable<BO.ProductItem> BOprod)
    {
        InitializeComponent();
        BOproducts = BOprod;
        bocart = cart;
        PoProduct = pro;
        DataContext = PoProduct;
        //PoProduct.Amount=bl.Order
        int[] numArray = new int[20];
        for (int i = 0; i < 20; i++)
            numArray[i] = i + 1;
        AmountOfProduct.ItemsSource = numArray;
        AmountOfProduct.SelectedItem = 1;
        products = prod;
        BoProduct = PL.Tools.CopyProp<PO.ProductItemPO, BO.ProductItem>(PoProduct);
    }

    private void AddTocart_Click(object sender, RoutedEventArgs e)
    {
        //BO.Cart cart = new BO.Cart();
        int id = PoProduct.ID;
        int amount = (int)AmountOfProduct.SelectedItem;
        double price = PoProduct.Price ?? 0;
        try
        {
            if (PoProduct.Amount > 0)
                bl.Cart.UpdateAmountOfProductInCart(bocart, id, amount);
            else bl.Cart.AddProductToCart(bocart, id, amount);

            //bocart.Items.Add(new PO.OrderItemPO() { ProductID = id,Name= PoProduct.Name, Amount = amount, Price = price, IsDeleted=false, Path= PoProduct.Path});
            //pocart.TotalPrice += price * amount;
            //bl.Cart.AddProductToCart(cart,id, amount);
            MessageBox.Show("Add To Cart Seccessfully", "Add To Cart", MessageBoxButton.OK);
            products.Remove(PoProduct);
            PoProduct.Amount = amount;
            products.Add(PoProduct);
            BOproducts.ToList().Remove(BoProduct);
            BoProduct = PoProduct.CopyFields(BoProduct);
            BoProduct.Amount = amount;
            BOproducts.ToList().Add(BoProduct);
            DataContext = PoProduct;
            //this.NavigationService.GoBack();
        }
        catch (BO.NotExistException)
        {
            MessageBox.Show("The Product Does Not Exist", "Not Exist", MessageBoxButton.OK);
        }
        catch (BO.NotInStockException)
        {
            MessageBox.Show("Sorry! It Is Out Of Stock", "ERROR", MessageBoxButton.OK);
        }
        
    }
    private void back_click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }


}



