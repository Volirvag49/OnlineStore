using System;

namespace Store.ApiStore.Infrastructure.Exceptions.Base
{
    public abstract class StoreServiceException : Exception
    {
        protected StoreServiceException(string message) : base(message)
        {
        }

        protected StoreServiceException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
