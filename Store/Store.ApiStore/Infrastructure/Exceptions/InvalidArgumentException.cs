using Store.ApiStore.Infrastructure.Exceptions.Base;

namespace Store.ApiStore.Infrastructure.Exceptions
{
    public class InvalidArgumentException : StoreServiceException
    {
        public InvalidArgumentException(string message) : base(message)
        {
        }

        public InvalidArgumentException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
