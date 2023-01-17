using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace PO;

public class ProductItemPO: INotifyPropertyChanged
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

    private int amount;

    public int Amount
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


    private string? name;
    public string? Name
    {
        get
        { return name; }
        set
        {
            name = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
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

    private Category category;
    public Category Category
    {
        get
        { return category; }
        set
        {
            category = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));
            }
        }
    }

    private bool? isInStock;

    public bool? IsInStock
    {
        get
        { return isInStock; }
        set
        {
            isInStock = value;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("IsInStock"));
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
