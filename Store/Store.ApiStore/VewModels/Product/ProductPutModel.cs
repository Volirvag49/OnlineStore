using System;

namespace Store.ApiStore.VewModels.Product
{
    public class ProductPutModel: ProductPostModel
    {
        public Guid Id { get; set; }
    }
}
