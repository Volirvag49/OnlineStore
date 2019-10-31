using System;

namespace Store.Database.Entities.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
