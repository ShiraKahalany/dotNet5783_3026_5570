using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BO;

[Serializable]
public class MyException : Exception
{
    public MyException() : base() { }
    public MyException(string message) : base(message) { }
    public MyException(string message, Exception innerException) : base(message, innerException) { }
    protected MyException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class NotExistException : Exception
{
    public NotExistException() : base() { }
    public NotExistException(string message) : base(message) { }
    public NotExistException(string message, Exception innerException) : base(message, innerException) { }
    protected NotExistException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class InAnOrderException : Exception
{
    public InAnOrderException() : base() { }
    public InAnOrderException(string message) : base(message) { }
    public InAnOrderException(string message, Exception innerException) : base(message, innerException) { }
    protected InAnOrderException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
    public class NoItemsException : Exception
    {
        //public MyExceptionNoItems() : base() {}
        public NoItemsException(string message ="No Items!") : base(message) { }
        public NoItemsException(string message, Exception innerException) : base(message, innerException) { }
        protected NoItemsException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
    }

    [Serializable]
public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    protected AlreadyExistException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}