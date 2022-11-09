using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        int Add(T item);
        T Get(int id);
        void Update(T item);
        void Delete(int id);

        //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
        IEnumerable<T> GetAll();


    }
}
