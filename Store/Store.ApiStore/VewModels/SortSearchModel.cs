using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.ApiStore.VewModels
{
    public class SortSearchModel
    {
        public string SearchString { get; set; }
        public string SearchSelection { get; set; }

        public string SortOrder { get; set; }
        public bool ByDescending { get; set; }

        public int Page { get; set; }
        public int pageSize { get; set; }

    }
}
