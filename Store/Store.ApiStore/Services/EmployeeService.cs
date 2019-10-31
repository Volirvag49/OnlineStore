using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.ApiStore.Infrastructure.Exceptions;
using Store.ApiStore.Services.Base;
using Store.ApiStore.VewModels.Customer;
using Store.Database.Entities;
using Store.Database.Repositories.Base;

namespace Store.ApiStore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IReadOnlyRepository _readOnly;
        private readonly IWriteOnlyRepository _writeOnly;
        private readonly IMapper _mapper;
        public EmployeeService(IReadOnlyRepository readOnly,
            IWriteOnlyRepository writeOnly,
            IMapper mapper)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
            _mapper = mapper;
        }

        public async Task<CustomerGetModel> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidArgumentException($"Id is not a valid {nameof(Guid)}!");

            var result = await _readOnly.GetFirstAsync<Customer>(q => q.Id == id);

            if (result == null)
                throw new NotFoundException($"Can't find a {typeof(Customer).Name} with ID = {id}");

            return _mapper.Map<CustomerGetModel>(result);
        }

        public async Task<CustomerGetModel[]> GetByAll()
        {
            var result = await _readOnly.GetAllAsync<Customer>();
            return _mapper.Map<CustomerGetModel[]>(result);
        }

        public async Task<CustomerGetModel[]> GetAllRemoved()
        {
            var result = await _readOnly.GetAllAsync<Customer>(
                isDeleted: true, isIgnoreQueryFilter: true);
            return _mapper.Map<CustomerGetModel[]>(result);
        }

        public async Task<Guid> Create(CustomerPostModel postModel)
        {
            if(postModel == null)
                throw new InvalidArgumentException($"{typeof(CustomerPutModel).Name} was null!");

            var entity = _mapper.Map<Customer>(postModel);
            await _writeOnly.SaveChangesAsync(entity);

            return entity.Id;
        }

        public async Task Update(CustomerPutModel putModel)
        {
            if (putModel == null)
                throw new InvalidArgumentException($"{typeof(CustomerPutModel).Name} was null!");

            var item = await _readOnly.ExistsAsync<Customer>(q => q.Id == putModel.Id);
            if (item == false)
                throw new NotFoundException($"Can't find a {typeof(Customer).Name} with ID = {putModel.Id}");

            var entity = _mapper.Map<Customer>(putModel);

            await _writeOnly.SaveChangesAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidArgumentException($"Id is not a valid {nameof(Guid)}!");

            var item = await _readOnly.GetQueryable<Customer>(x => x.Id == id,
                isDeleted: null)
                .Select(x => new Customer
                {
                    Id = x.Id,
                    IsDeleted = x.IsDeleted
                }).FirstOrDefaultAsync();

            if (item == null)
                throw new NotFoundException($"Can't find a {typeof(Customer).Name} with ID = {id}!");

            if (item.IsDeleted == true)
                throw new LogicalException($"{typeof(Customer).Name} with ID = {id} was removed!");

            await _writeOnly.DeleteByIdAsync<Customer>(id);
        }

        public async Task Restore(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidArgumentException($"Id is not a valid {nameof(Guid)}!");

            var item = await _readOnly.GetQueryable<Customer>(x => x.Id == id,
                isDeleted: true, isIgnoreQueryFilter:true)
                .Select(x => new Customer
                {
                    Id = x.Id,
                    IsDeleted = x.IsDeleted
                }).FirstOrDefaultAsync();

            if (item == null)
                throw new NotFoundException($"Can't find a {typeof(Customer).Name} with ID = {id}!");

            if (item.IsDeleted == false)
                throw new LogicalException($"{typeof(Customer).Name} with ID = {id} is not removed!");

            await _writeOnly.RestoreByIdAsync<Customer>(id);
        }
    }
}
