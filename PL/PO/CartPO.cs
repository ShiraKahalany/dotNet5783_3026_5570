using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
//using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PO;

 public class CartPO : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string? customerName;
    public string? CustomerName
    { 
        get
        { return customerName; }
        set
        {
            customerName = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerName"));
            }
        }
    }

    private string? customerEmail;
    public string? CustomerEmail
    {
        get
        { return customerEmail; }
        set
        {
            customerEmail = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(" CustomerEmail"));
            }
        }
    }

    private string? customerAddress;
    public string? CustomerAddress
    {
        get
        { return customerAddress; }
        set
        {
            customerAddress = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerAddress"));
            }
        }
    }

    private ObservableCollection<PO.OrderItemPO>? items;
    public ObservableCollection<PO.OrderItemPO>? Items
    {
        get
        { return items; }
        set
        {
            items = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("items"));
            }
        }
    }

    private double? totalPrice;
    public double? TotalPrice
    {
        get
        { return totalPrice; }
        set
        {
            totalPrice = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }
    }
}
