using System;

namespace Store.ApiStore.VewModels.Product
{
    public class ProductGetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
    }
}
