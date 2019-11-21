﻿using System;
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

        Task<Object> GetWithFilterAndSotring(SortSearchModel sortSearchModel);

        Task<Guid> Create(ProductPostModel pustModel);
        Task Update(ProductPutModel putModel);
        Task Delete(Guid id);
        Task Restore(Guid id);
    }
}
