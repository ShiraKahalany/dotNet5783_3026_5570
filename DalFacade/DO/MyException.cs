using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

//מחלקה לחריגות בשכבת הנתונים
namespace DO
{
    [Serializable]
    public class MyException : Exception
    {
        public MyException() : base() { }
        public MyException(string message) : base(message) { }  
        public MyException(string message, Exception innerException) : base(message, innerException) { }    
        protected MyException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
    }
}
