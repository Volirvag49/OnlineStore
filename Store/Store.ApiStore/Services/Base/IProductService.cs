using System;
using System.Threading.Tasks;
using Store.ApiStore.VewModels;
using Store.ApiStore.VewModels.Product;

namespace Store.ApiStore.Services.Base
{
    public interface IProductService
    {
        Task<ProductGetModel> GetById(Guid id);
        Task<ProductGetModel[]> GetByAll();

        Task<ProductGetModel[]> GetAllRemoved();

        Task<ResponceModel<ProductGetModel>> Search(RequestModel sortSearchModel);

        Task<Guid> Create(ProductPostModel pustModel);
        Task Update(ProductPutModel putModel);
        Task Delete(Guid id);
        Task Restore(Guid id);
    }
}
