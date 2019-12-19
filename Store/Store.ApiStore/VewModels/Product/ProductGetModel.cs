using System;
using Store.ApiStore.VewModels.Image;

namespace Store.ApiStore.VewModels.Product
{
    public class ProductGetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ImageGetModel Image { get; set; }
    }
}
