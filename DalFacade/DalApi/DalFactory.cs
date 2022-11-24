using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public class DalFactory
{
    public static IDal? GetDal()
    {
        IDal? dal = new Instance;
    }
    internal static DataSource s_instance { get; } = new DataSource();   //יצירת מופע נתונים

}
