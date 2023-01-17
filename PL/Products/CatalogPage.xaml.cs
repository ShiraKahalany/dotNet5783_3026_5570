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
    //private PO.CartPO pocart;
    private BO.Cart bocart;
    public CatalogPage(string category, Frame mainFrame, BO.Cart bcart)
    {
        InitializeComponent();
        bocart = bcart;
        switch (category)
        {
            case "kitchen":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Kitchen);
                break;
            case "living_room":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Living_room);
                break;
            case "bath_room":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Bathroom);
                break;
            case "bed_room":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Bedroom);
                break;
            case "garden":
                BOproducts = bl.Product.GetProductItemsList(bocart, BO.Filters.filterByCategory, BO.Category.Garden);
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
    private void ProductDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e) => frame.Content = new ProductDetails(((PO.ProductItemPO)listCatalog.SelectedItem), bocart, itemsList, BOproducts);

}
