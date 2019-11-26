using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.ApiStore.VewModels
{
    public class RequestModel
    {
        public string SearchString { get; set; }
        public string SearchSelection { get; set; }

        public string SortOrder { get; set; }
        public bool ByDescending { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

    }
}
