using System;
using Store.Database.Entities.Base;

namespace Store.Database.Entities
{
    public class CartItem: Entity, IEntity
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
