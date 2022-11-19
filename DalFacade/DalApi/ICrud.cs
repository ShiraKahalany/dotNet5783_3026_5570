

namespace DalApi;

public interface ICrud<T> where T : struct
    //ממשק גנרי ICrud, שכולל את הפעולות הבסיסיות עבור כל ישות
{
    int Add(T item);  //הוספת ישות
    T GetByID(int id);  //קבלת ישות לפי המספר המזהה שלה
    void Update(T item);  //עידכון פרטי ישות קיימת
    void Delete(int id); //

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<T> GetAll();  //קבלת כל העצמים הקיימים מישות מסויימת

    IEnumerable<T> GetAllWithDeleted();  //קבלת כל העצמים הקיימים מישות מסויימת, כולל אלו שנמחקו בעבר

}
