using DO;


namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
    //ממשק עבור פעולות בסיסיות על פריט בהזמנה. מקבל את המתודות של ICrud, ומכיל כאן מתודה אחת נוספת
{ 
    OrderItem? GetByOrderAndId(int orderId, int productId);  //מתודה להחזרת מוצר-בהזמנה על פי מספר הזמנה ומוצר
    public IEnumerable<OrderItem?> GetAll(int id);
    //IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null);
   
}
