using System.Collections.Generic;

namespace Store.Database.Entities.Base
{
    public class PagedResult<T> : PagedResultBase where T : IEntity
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
