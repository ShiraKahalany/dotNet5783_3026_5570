using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;


//ממשק עבור סל קניות
public interface ICart
{
    public BO.Cart? AddProductToCart (BO.Cart cart, int id, int amountToAdd = 1 );  //מתודת הוספת מוצר לסל קניות
    public BO.Cart? UpdateAmountOfProductInCart(BO.Cart cart, int id, int amount);  //מתודה לעידכון כמות של מוצר בסל הקניות
    public int MakeAnOrder(BO.Cart? cart);   //מתודה להזמנת סל קניות
   
    
}
