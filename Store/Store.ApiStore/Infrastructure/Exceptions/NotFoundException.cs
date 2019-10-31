using System;
using Store.ApiStore.Infrastructure.Exceptions.Base;

namespace Store.ApiStore.Infrastructure.Exceptions
{
    public class NotFoundException : StoreServiceException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
