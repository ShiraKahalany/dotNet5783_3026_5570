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
public class MyExceptionNotExist : Exception
{
    public MyExceptionNotExist() : base() { }
    public MyExceptionNotExist(string message) : base(message) { }
    public MyExceptionNotExist(string message, Exception innerException) : base(message, innerException) { }
    protected MyExceptionNotExist(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class MyExceptionInAnOrder : Exception
{
    public MyExceptionInAnOrder() : base() { }
    public MyExceptionInAnOrder(string message) : base(message) { }
    public MyExceptionInAnOrder(string message, Exception innerException) : base(message, innerException) { }
    protected MyExceptionInAnOrder(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
    public class MyExceptionNoItems : Exception
    {
        //public MyExceptionNoItems() : base() {}
        public MyExceptionNoItems(string message ="No Items!") : base(message) { }
        public MyExceptionNoItems(string message, Exception innerException) : base(message, innerException) { }
        protected MyExceptionNoItems(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
    }

    [Serializable]
public class MyExceptionAlreadyExist : Exception
{
    public MyExceptionAlreadyExist() : base() { }
    public MyExceptionAlreadyExist(string message) : base(message) { }
    public MyExceptionAlreadyExist(string message, Exception innerException) : base(message, innerException) { }
    protected MyExceptionAlreadyExist(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}