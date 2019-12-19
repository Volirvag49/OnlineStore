using Store.ApiStore.VewModels.Image;

namespace Store.ApiStore.VewModels.Product
{
    public class ProductPostModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ImagePostModel Image { get; set; }
    }
}
