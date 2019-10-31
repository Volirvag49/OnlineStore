using System;
using System.Collections.Generic;
using System.Text;
using Store.Database.Entities.Base;

namespace Store.Database.Entities
{
    public class Customer : Entity, IEntity
    {
        public string FirstName {get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
