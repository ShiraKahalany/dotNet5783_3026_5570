using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
//using System.Collections.Generic;
using System.ComponentModel;

namespace PO;

public class OrderItemPO : INotifyPropertyChanged
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

    private int? productID;
    public int? ProductID
    {
        get
        { return productID; }
        set
        {
            productID = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ProductID"));
            }
        }
    }

    private double? price;
    public double? Price
    {
        get
        { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
            }
        }
    }

    private int? amount;
    public int? Amount
    {
        get
        { return amount; }
        set
        {
            amount = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
            }
        }
    }

   

    private string? path;

    public string? Path
    {
        get
        { return path; }
        set
        {
            path = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Path"));
            }
        }
    }


}
