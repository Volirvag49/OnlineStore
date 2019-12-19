using System;
using Store.Database.Entities.Base;

namespace Store.Database.Entities
{
    public class Image : Entity, IEntity
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime LastModified { get; set; }
        public string FileAsBase64 { get; set; }

        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
