using AutoMapper;
using Store.ApiStore.VewModels;
using Store.ApiStore.VewModels.Customer;
using Store.ApiStore.VewModels.Image;
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
            CreateMap<PagedResult<Product>, ResponceModel<ProductGetModel>>()
                .ForMember(d => d.Results,
                m => m.MapFrom(s => s.Results));

            // Customer
            CreateMap<Customer, CustomerGetModel>();
            CreateMap<CustomerPostModel, Customer>();
            CreateMap<CustomerPutModel, Customer>();

            // Product
            CreateMap<Product, ProductGetModel>();
            CreateMap<ProductPostModel, Product>();
            CreateMap<ProductPutModel, Product>()
                .ForMember(d => d.Image,
                m => m.MapFrom(s => s.Image))
                .ForPath(d => d.Image.ProductId,
                m => m.MapFrom(s => s.Id));


            // Image             
            CreateMap<Image, ImageGetModel>();
            CreateMap<ImagePostModel, Image>();
            CreateMap<ImagePutModel, Image>();
        }
    }
}
