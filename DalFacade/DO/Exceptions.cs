using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

//מחלקה לחריגות בשכבת הנתונים
namespace DO;


[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException() : base() { }
    public DalConfigException(string message) : base(message) { }
    public DalConfigException(string message, Exception innerException) : base(message, innerException) { }
    protected DalConfigException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
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
public class AlreadyExistException : Exception
{
    public AlreadyExistException() : base() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    protected AlreadyExistException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

