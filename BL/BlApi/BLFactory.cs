using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BlImplementation;
namespace BlApi;


//יצרן שכבת הלוגיקה
public static class BLFactory
{
    public static IBL GetBL() => BL.instance;  
}
