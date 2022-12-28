using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
public class FactoryDal
{
    public static IDAL GetDal()
    { 
        return new Dal_XML(); 
    }
}
}
