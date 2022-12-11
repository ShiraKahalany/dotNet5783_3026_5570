

using DO;

namespace DalApi;

public interface ICrud<T> where T : struct
    //ממשק גנרי ICrud, שכולל את הפעולות הבסיסיות עבור כל ישות
{
    int Add(T item);  //הוספת ישות
    T GetByID(int id);  //קבלת ישות לפי המספר המזהה שלה
    T GetDeletedById(int id);
    void Update(T item);  //עידכון פרטי ישות קיימת
    void Delete(int id); //
  void DeletePermanently(int id);
    void Restore(T item);

    IEnumerable<T?> GetAll();  //קבלת כל העצמים הקיימים מישות מסויימת

    IEnumerable<T?> GetAllWithDeleted();  //קבלת כל העצמים הקיימים מישות מסויימת, כולל אלו שנמחקו בעבר

    IEnumerable<T?> GetAllDeleted();
    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    T? GetTByFilter(Func<T?, bool> filter);


}
