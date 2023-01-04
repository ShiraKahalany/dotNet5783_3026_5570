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

namespace PL.Orders;

/// <summary>
/// Interaction logic for OrderTrackingByID.xaml
/// </summary>
public partial class OrderTrackingByID : Page
{
    Frame myframe;
    public OrderTrackingByID(Frame frame)
    {
        InitializeComponent();
        myframe = frame;
    }

    private void CloseManagerLogIn_Click(object sender, RoutedEventArgs e)
    {
        managerButton.Visibility = Visibility.Visible;
        managerButton.IsEnabled = true;
        PasswordBox.Password = "";
    }

    private void ManagerLogIn_Click(object sender, RoutedEventArgs e)
    {
        managerButton.Visibility = Visibility.Hidden;
        managerButton.IsEnabled = false;

    }

    private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
    {
        //if (e.LeftButton == MouseButtonState.Pressed)
        //{
        //    DragMove();
        //}
    }

    // private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

    private void ManagerlogInWithPassword_Click(object sender, RoutedEventArgs e)
    {
        EnterPassword();

    }

    private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) EnterPassword();
    }

    private void EnterPassword()
    {
        if (PasswordBox.Password == "1234")
        {
            OrderTracking orderTracking = new();
            PasswordBox.Password = "";
            myframe.Content = orderTracking;
        }
    }

}
