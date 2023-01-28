using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL.Products;

/// <summary>
/// Interaction logic for CatalogPage.xaml
/// </summary>
public partial class CatalogPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private IEnumerable<BO.ProductItem> BOproducts;
    private List<PO.ProductItemPO> itemsList;
    Frame frame;
    private BO.Cart bocart;
    public CatalogPage(string category, Frame mainFrame, BO.Cart bcart)
    {
        InitializeComponent();
        bocart = bcart;
        BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory);
        var x = from product in BOproducts
                group product by product.Category;
        switch (category)
        {
            case "kitchen":
                BOproducts = (IEnumerable<BO.ProductItem>)x.Where(g => g.Key == BO.Category.Kitchen).SelectMany(g=>g);
                break;
            case "living_room":
                BOproducts = (IEnumerable<BO.ProductItem>)x.Where(g => g.Key == BO.Category.Living_room).SelectMany(g => g);
                break;
            case "bath_room":
                BOproducts = (IEnumerable<BO.ProductItem>)x.Where(g => g.Key == BO.Category.Bathroom).SelectMany(g => g);
                break;
            case "bed_room":
                BOproducts = (IEnumerable<BO.ProductItem>)x.Where(g => g.Key == BO.Category.Bedroom).SelectMany(g => g);
                break;
            case "garden":
                BOproducts = (IEnumerable<BO.ProductItem>)x.Where(g => g.Key == BO.Category.Garden).SelectMany(g => g);
                break;
            case "all":
                BOproducts = bl.Product.GetProductItemsList(bocart);
                itemsList = (from BO.ProductItem p in BOproducts select Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>(p)).ToList();
                listCatalog.ItemsSource = itemsList;
                ListCollectionView view = (ListCollectionView)CollectionViewSource.GetDefaultView(listCatalog.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
                view.GroupDescriptions.Add(groupDescription);
                break;
        }
        if (category != "all")
            itemsList = (from BO.ProductItem p in BOproducts select Tools.CopyProp<BO.ProductItem, PO.ProductItemPO>(p)).ToList();
        listCatalog.DataContext = itemsList;
        frame = mainFrame;
    }

    #region ProductDetails_MouseDoubleClick
    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e) => frame.Content = new ProductDetails(((PO.ProductItemPO)listCatalog.SelectedItem), bocart, itemsList, BOproducts);
    #endregion
}
