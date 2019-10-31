using System;

namespace Store.ApiStore.VewModels.Customer
{
    public class CustomerPutModel : CustomerPostModel
    {
        public Guid Id { get; set; }
    }
}
