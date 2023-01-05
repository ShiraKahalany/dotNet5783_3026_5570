using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
//using System.Collections.Generic;
using System.ComponentModel;
namespace PO;

internal class OrderForListPO : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

   

    private int id;

    public int ID
    {
        get
        { return id; }
        set
        {
            id = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }
    }

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

   

    private OrderStatus? status;
    public OrderStatus? Status
    {
        get
        { return status; }
        set
        {
            status = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
    }

    private int? amountOfItems;
    public int? AmountOfItems
    {
        get
        { return amountOfItems; }
        set
        {
            amountOfItems = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("AmountOfItems"));
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
