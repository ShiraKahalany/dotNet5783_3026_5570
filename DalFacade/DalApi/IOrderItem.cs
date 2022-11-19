using DO;


namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
    //ממשק עבור פעולות בסיסיות על פריט בהזמנה. מקבל את המתודות של ICrud, ומכיל כאן מתודה אחת נוספת
{ 
    OrderItem GetByOrderAndId(int orderId, int productId);  //מתודה להחזרת מוצר-בהזמנה על פי מספר הזמנה ומוצר

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    //IEnumerable<OrderItem> GetAll(int id);
}
