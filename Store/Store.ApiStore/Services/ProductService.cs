using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.ApiStore.Infrastructure.Exceptions;
using Store.ApiStore.Services.Base;
using Store.ApiStore.VewModels.Product;
using Store.Database.Entities;
using Store.Database.Repositories.Base;

namespace Store.ApiStore.Services
{
    public class ProductService: IProductService
    {

        private readonly IReadOnlyRepository _readOnly;
        private readonly IWriteOnlyRepository _writeOnly;
        private readonly IMapper _mapper;
        public ProductService(IReadOnlyRepository readOnly,
            IWriteOnlyRepository writeOnly,
            IMapper mapper)
        {
            _readOnly = readOnly;
            _writeOnly = writeOnly;
            _mapper = mapper;
        }
        public async Task<ProductGetModel> GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidArgumentException($"Id is not a valid {nameof(Guid)}!");

            var result = await _readOnly.GetFirstAsync<Product>(q => q.Id == id);

            if (result == null)
                throw new NotFoundException($"Can't find a {typeof(Product).Name} with ID = {id}");

            return _mapper.Map<ProductGetModel>(result);
        }

        public async Task<ProductGetModel[]> GetByAll()
        {
            var result = await _readOnly.GetAllAsync<Product>();
            return _mapper.Map<ProductGetModel[]>(result);
        }

        public async Task<ProductGetModel[]> GetAllRemoved()
        {
            var result = await _readOnly.GetAllAsync<Product>(
                isDeleted: true, isIgnoreQueryFilter: true);
            return _mapper.Map<ProductGetModel[]>(result);
        }

        public async Task<Guid> Create(ProductPostModel postModel)
        {
            if (postModel == null)
                throw new InvalidArgumentException($"{typeof(ProductPostModel).Name} was null!");

            var entity = _mapper.Map<Product>(postModel);
            await _writeOnly.SaveChangesAsync(entity);

            return entity.Id;
        }

        public async Task Update(ProductPutModel putModel)
        {
            if (putModel == null)
                throw new InvalidArgumentException($"{typeof(ProductPutModel).Name} was null!");

            var item = await _readOnly.ExistsAsync<Product>(q => q.Id == putModel.Id);
            if (item == false)
                throw new NotFoundException($"Can't find a {typeof(Product).Name} with ID = {putModel.Id}");

            var entity = _mapper.Map<Product>(putModel);

            await _writeOnly.SaveChangesAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidArgumentException($"Id is not a valid {nameof(Guid)}!");

            var item = await _readOnly.GetQueryable<Product>(x => x.Id == id,
                isDeleted: null)
                .Select(x => new Product
                {
                    Id = x.Id,
                    IsDeleted = x.IsDeleted
                }).FirstOrDefaultAsync();

            if (item == null)
                throw new NotFoundException($"Can't find a {typeof(Product).Name} with ID = {id}!");

            if (item.IsDeleted == true)
                throw new LogicalException($"{typeof(Product).Name} with ID = {id} was removed!");

            await _writeOnly.DeleteByIdAsync<Product>(id);
        }

        public async Task Restore(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidArgumentException($"Id is not a valid {nameof(Guid)}!");

            var item = await _readOnly.GetQueryable<Product>(x => x.Id == id,
                isDeleted: true, isIgnoreQueryFilter: true)
                .Select(x => new Product
                {
                    Id = x.Id,
                    IsDeleted = x.IsDeleted
                }).FirstOrDefaultAsync();

            if (item == null)
                throw new NotFoundException($"Can't find a {typeof(Product).Name} with ID = {id}!");

            if (item.IsDeleted == false)
                throw new LogicalException($"{typeof(Product).Name} with ID = {id} is not removed!");

            await _writeOnly.RestoreByIdAsync<Product>(id);
        }
    }
}
