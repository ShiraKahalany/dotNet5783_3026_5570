using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
//using System.Collections.Generic;
using System.ComponentModel;

namespace PO;

public class OrderPO : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private bool isDeleted;

    public bool IsDeleted
    {
        get
        { return isDeleted; }
        set
        {
            isDeleted = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsDeleted"));
            }
        }
    }


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

    private DateTime? orderDate;
    public DateTime? OrderDate
    {
        get
        { return orderDate; }
        set
        {
            orderDate = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(" OrderDate"));
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

    private DateTime? shipDate;
    public DateTime? ShipDate
    {
        get
        { return shipDate; }
        set
        {
            shipDate = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(" ShipDate"));
            }
        }
    }

    private DateTime? deliveryDate;
    public DateTime? DeliveryDate
    {
        get
        { return deliveryDate; }
        set
        {
            deliveryDate = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(" DeliveryDate"));
            }
        }
    }

    private List<OrderItem>? items;
    public List<OrderItem>? Items
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
