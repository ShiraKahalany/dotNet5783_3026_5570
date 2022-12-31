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
    public CustomerCart(PO.CartPO cartPO)
    {
        InitializeComponent();
        
        
    }
}
