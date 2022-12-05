using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BO;


[Serializable]
public class NotExistException : Exception
{
    public NotExistException() : base() { }
    public NotExistException(string message) : base(message) { }
    public NotExistException(string message, Exception innerException) : base(message, innerException) { }
    protected NotExistException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class OrderNotExistException : Exception
{
    public OrderNotExistException() : base() { }
    public OrderNotExistException(string message) : base(message) { }
    public OrderNotExistException(string message, Exception innerException) : base(message, innerException) { }
    protected OrderNotExistException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
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

[Serializable]
public class NotInStockException : Exception
{
    public NotInStockException() : base() { }
    public NotInStockException(string message) : base(message) { }
    public NotInStockException(string message, Exception innerException) : base(message, innerException) { }
    protected NotInStockException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class NoNameException : Exception
{
    public NoNameException() : base() { }
    public NoNameException(string message) : base(message) { }
    public NoNameException(string message, Exception innerException) : base(message, innerException) { }
    protected NoNameException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class IllegalEmailException : Exception
{
    public IllegalEmailException() : base() { }
    public IllegalEmailException(string message) : base(message) { }
    public IllegalEmailException(string message, Exception innerException) : base(message, innerException) { }
    protected IllegalEmailException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}


[Serializable]
public class IllegalIdException : Exception
{
    public IllegalIdException() : base() { }
    public IllegalIdException(string message) : base(message) { }
    public IllegalIdException(string message, Exception innerException) : base(message, innerException) { }
    protected IllegalIdException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class AmountNotPossitiveException : Exception
{
    public AmountNotPossitiveException() : base() { }
    public AmountNotPossitiveException(string message) : base(message) { }
    public AmountNotPossitiveException(string message, Exception innerException) : base(message, innerException) { }
    protected AmountNotPossitiveException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class NoAddressException : Exception
{
    public NoAddressException() : base() { }
    public NoAddressException(string message) : base(message) { }
    public NoAddressException(string message, Exception innerException) : base(message, innerException) { }
    protected NoAddressException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class OrderHasShippedException : Exception
{
    public OrderHasShippedException() : base() { }
    public OrderHasShippedException(string message) : base(message) { }
    public OrderHasShippedException(string message, Exception innerException) : base(message, innerException) { }
    protected OrderHasShippedException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class OrderHasDeliveredException : Exception
{
    public OrderHasDeliveredException() : base() { }
    public OrderHasDeliveredException(string message) : base(message) { }
    public OrderHasDeliveredException(string message, Exception innerException) : base(message, innerException) { }
    protected OrderHasDeliveredException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class OrderHasNotShippedException : Exception
{
    public OrderHasNotShippedException() : base() { }
    public OrderHasNotShippedException(string message) : base(message) { }
    public OrderHasNotShippedException(string message, Exception innerException) : base(message, innerException) { }
    protected OrderHasNotShippedException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class CanNotUpdateOrderException : Exception
{
    public CanNotUpdateOrderException() : base() { }
    public CanNotUpdateOrderException(string message) : base(message) { }
    public CanNotUpdateOrderException(string message, Exception innerException) : base(message, innerException) { }
    protected CanNotUpdateOrderException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class NotItemsInCartException : Exception
{
    public NotItemsInCartException() : base() { }
    public NotItemsInCartException(string message) : base(message) { }
    public NotItemsInCartException(string message, Exception innerException) : base(message, innerException) { }
    protected NotItemsInCartException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}


[Serializable]
public class WrongDetailsException : Exception
{
    public WrongDetailsException() : base() { }
    public WrongDetailsException(string message) : base(message) { }
    public WrongDetailsException(string message, Exception innerException) : base(message, innerException) { }
    protected WrongDetailsException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}

[Serializable]
public class CantCancelOrderException : Exception
{
    public CantCancelOrderException() : base() { }
    public CantCancelOrderException(string message) : base(message) { }
    public CantCancelOrderException(string message, Exception innerException) : base(message, innerException) { }
    protected CantCancelOrderException(SerializationInfo info, StreamingContext contex) : base(info, contex) { }
}