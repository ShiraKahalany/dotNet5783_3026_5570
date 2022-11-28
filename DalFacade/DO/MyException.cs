using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

//מחלקה לחריגות בשכבת הנתונים
namespace DO;

[Serializable]
public class MyException : Exception
{
    public MyException() : base() { }
    public MyException(string message) : base(message) { }  
    public MyException(string message, Exception innerException) : base(message, innerException) { }    
    protected MyException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException() : base() { }
    public DalConfigException(string message) : base(message) { }
    public DalConfigException(string message, Exception innerException) : base(message, innerException) { }
    protected DalConfigException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
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
public class MyExceptionAlreadyExist : Exception
{
    public MyExceptionAlreadyExist() : base() { }
    public MyExceptionAlreadyExist(string message) : base(message) { }
    public MyExceptionAlreadyExist(string message, Exception innerException) : base(message, innerException) { }
    protected MyExceptionAlreadyExist(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

