using Store.ApiStore.Infrastructure.Exceptions.Base;

namespace Store.ApiStore.Infrastructure.Exceptions
{
    public class LogicalException : StoreServiceException
    {
        public LogicalException(string message) : base(message)
        {
        }

        public LogicalException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
