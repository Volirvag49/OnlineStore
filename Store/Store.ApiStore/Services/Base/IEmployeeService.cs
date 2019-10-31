using System;
using System.Threading.Tasks;
using Store.ApiStore.VewModels.Customer;

namespace Store.ApiStore.Services.Base
{
    public interface IEmployeeService
    {
        Task<CustomerGetModel> GetById(Guid id);
        Task<CustomerGetModel[]> GetByAll();

        Task<CustomerGetModel[]> GetAllRemoved();

        Task<Guid> Create(CustomerPostModel pustModel);
        Task Update(CustomerPutModel putModel);
        Task Delete(Guid id);
        Task Restore(Guid id);
    }
}
