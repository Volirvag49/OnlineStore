using AutoMapper;
using Store.ApiStore.VewModels;
using Store.ApiStore.VewModels.Customer;
using Store.ApiStore.VewModels.Product;
using Store.Database.Entities;
using Store.Database.Entities.Base;

namespace Store.ApiStore.Infrastructure.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // pagination
            CreateMap<PagedResult<Product>, PagedViewModel<ProductGetModel>>()
                .ForMember(x => x.Results,
                x => x.MapFrom(m => m.Results));

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
