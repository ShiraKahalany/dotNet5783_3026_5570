using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum Category { Living_room, Bathroom, Kitchen, Bedroom, Garden, All }  //אינאם עבור קטגוריות המוצרים בחנות
public enum OrderStatus {Ordered, Shipped,Delivered, None} //אינאם עבור הסטטוסים האפשריים עבור הזמנה
public enum Filters { filterByCategory, filterByName, None }