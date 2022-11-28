using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    public BO.Cart AddProductToCart (BO.Cart cart,int id);
    public BO.Cart UpdateAmountOfProductInCart(BO.Cart cart, int id, int amount);

    public void MakeAnOrder(BO.Cart cart);
    
}
