using BlApi;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PO;
namespace PL;

/// <summary>
/// Interaction logic for ManagerProductsPage.xaml
/// </summary>
public partial class ManagerProductsPage : Page
{
    private IBL bl = BLFactory.GetBL();
    private ObservableCollection<PO.ProductPO> observeproducts = new ObservableCollection<PO.ProductPO>();
    private IEnumerable<BO.Product> BOproducts;
    Frame myframe;
    public ManagerProductsPage(Frame MainManagerOptionsFrame)
    {
        InitializeComponent();
        myframe = MainManagerOptionsFrame;
        BOproducts = bl.Product.GetProducts();
        observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
        //ProductListView.DataContext = observeproducts;
        ProductListView.ItemsSource = observeproducts;
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }


    private void AddProduct_Click(object sender, RoutedEventArgs e) => new Products.AddProduct(observeproducts).ShowDialog();

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if ((BO.Category)AttributeSelector.SelectedItem == BO.Category.All)
            BOproducts = bl.Product.GetProducts().ToList();
        else
            BOproducts = bl.Product.GetProducts(BO.Filters.filterByCategory, (BO.Category)AttributeSelector.SelectedItem).ToList();
        observeproducts.Clear();
        observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
        // ProductListView.DataContext = observeproducts;
    }

    private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        new ProductUpdateAndActions((PO.ProductPO)ProductListView.SelectedItem, observeproducts).ShowDialog();
        if ((AttributeSelector.SelectedItem!=null) && (BO.Category)AttributeSelector.SelectedItem != BO.Category.All)
        {
            BOproducts = bl.Product.GetProducts(BO.Filters.filterByCategory, (BO.Category)AttributeSelector.SelectedItem).ToList();
            observeproducts.Clear();
            observeproducts = BOproducts.ToObservableByConverter<BO.Product, PO.ProductPO>(observeproducts, PL.Tools.CopyProp<BO.Product, PO.ProductPO>);
        }
    }

    private void DeleteProduct_Click(object sender, RoutedEventArgs e)
    {
        PO.ProductPO po = ((Button)(sender)).DataContext as PO.ProductPO;
        int id = po?.ID ?? 0;
        bl.Product.DeleteProduct(id);
        observeproducts.Remove(po);
    }

    private void ShowDeletedProducts_Click(object sender, RoutedEventArgs e)
    {
        myframe.Content = new Manager.ProductsArchivePage(observeproducts);
    }
}
