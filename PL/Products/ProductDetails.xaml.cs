using BlApi;
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
    private List<PO.ProductItemPO> itemsList;
    private PO.ProductItemPO PoProduct;
    private BO.Cart bocart;

    public ProductDetails(PO.ProductItemPO pro, BO.Cart cart, List<PO.ProductItemPO> items, IEnumerable<BO.ProductItem> BOprod)
    {
        InitializeComponent();
        BOproducts = BOprod;
        bocart = cart;
        PoProduct = pro;
        DataContext = PoProduct;
        int[] numArray = new int[20];
        for (int i = 0; i < 20; i++)
            numArray[i] = i + 1;
        AmountOfProduct.ItemsSource = numArray;
        AmountOfProduct.SelectedItem = 1;
        itemsList = items;
        BoProduct = PL.Tools.CopyProp<PO.ProductItemPO, BO.ProductItem>(PoProduct);
    }

    #region AddToCart_Click
    private void AddTocart_Click(object sender, RoutedEventArgs e)
    {
        int id = PoProduct.ID;
        int amount = (int)AmountOfProduct.SelectedItem;
        double price = PoProduct.Price ?? 0;
        try
        {
            if (PoProduct.Amount > 0)
                bl.Cart.UpdateAmountOfProductInCart(bocart, id, amount);
            else bl.Cart.AddProductToCart(bocart, id, amount);
            itemsList.Remove(PoProduct);
            PoProduct.Amount = amount;
            itemsList.Add(PoProduct);
            BOproducts.ToList().Remove(BoProduct);
            BoProduct = PoProduct.CopyFields(BoProduct);
            BoProduct.Amount = amount;
            BOproducts.ToList().Add(BoProduct);
            DataContext = PoProduct;
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
    #endregion

    #region GoBack+Click
    private void back_click(object sender, RoutedEventArgs e)=> NavigationService.GoBack();
    #endregion

}



