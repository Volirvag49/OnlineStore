using AutoMapper;
using Store.ApiStore.VewModels.Customer;
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
        }
    }
}
