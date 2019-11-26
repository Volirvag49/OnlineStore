using System.Collections.Generic;
using Store.ApiStore.VewModels.Product;

namespace Store.ApiStore.VewModels
{
    public class ResponceModel<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int PageCount { get; set; }
        public int RowCount { get; set; }
        public int FirstRowOnPage { get; set; }
        public int LastRowOnPage { get; set; }

        public IList<T> Results { get; set; }
    }
}
