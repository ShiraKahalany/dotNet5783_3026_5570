using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
//ממשק ראשי המרכז את כל הממשקים של שכבת הלוגיקה
 public interface IBL
{
    public ICart Cart { get;}  //ממשק סלי הקניות
    public IOrder Order { get;}  //ממשק ההזמנות
    public IProduct Product { get;}   //ממשק המוצרים
}
