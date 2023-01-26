using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

//שכבת הלוגיקה
public class BL:IBL
{
    public static IBL instance { get; }=new BL();
    public BL() { }
    public IOrder Order { get; set; } =new Order();
    public IProduct Product { get; set; } = new Product();
    public ICart Cart { get; set; } = new Cart();

}
