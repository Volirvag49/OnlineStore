using System.Collections.Generic;
using Store.Database.Entities.Base;

namespace Store.Database.Entities
{
    public class Cart : Entity, IEntity
    {
        public Customer Customer { get; set; }

        public IEnumerable<CartItem> CartItems {get; set;}
    }
}
