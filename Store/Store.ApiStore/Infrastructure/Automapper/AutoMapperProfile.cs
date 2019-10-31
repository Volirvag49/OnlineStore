using AutoMapper;
using Store.ApiStore.VewModels.Customer;
using Store.ApiStore.VewModels.Product;
using Store.Database.Entities;

namespace Store.ApiStore.Infrastructure.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customer
            CreateMap<Customer, CustomerGetModel>();
            CreateMap<CustomerPostModel, Customer>();
            CreateMap<CustomerPutModel, Customer>();

            // Product
            CreateMap<Product, ProductGetModel>();
            CreateMap<ProductPostModel, Product>();
            CreateMap<ProductPutModel, Product>();
        }
    }
}
