using System.Collections.Generic;
using Store.Database.Entities.Base;

namespace Store.Database.Entities
{
    public class Product : Entity, IEntity
    {
        public string Name{ get; set;}
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
