using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;

//מחלקת דל-ליסט המממשת את הממשק איי-דל
 internal sealed class DalList : IDal
{
    public static IDal Instance { get; }=new DalList();  //יצירת אןבייקט מסוג DalList
    private DalList() { }   //בנאי (פרטי)
    public IOrder Order => new DalOrder() ;  //יצירת הזמנה
    public IProduct Product => new DalProduct() ;  //יצירת מוצר
    public IOrderItem OrderItem => new DalOrderItem() ;  //יצירת פריט-בהזמנה
}
