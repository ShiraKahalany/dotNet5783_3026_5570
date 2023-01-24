using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
    //ממשק גנרי ICrud, שכולל את הפעולות הבסיסיות עבור כל ישות
{
    int Add(T item);  //הוספת ישות
    void Update(T item);  //עידכון פרטי ישות קיימת
    void Delete(int id); //
  void DeletePermanently(int id);
    void Restore(T item);
    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    T? GetTByFilter(Func<T?, bool> filter);


}
