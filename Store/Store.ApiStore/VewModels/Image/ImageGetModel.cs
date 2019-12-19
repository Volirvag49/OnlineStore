using System;

namespace Store.ApiStore.VewModels.Image
{
    public class ImageGetModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime LastModified{ get; set; }
        public string FileAsBase64 { get; set; }
        public byte[] FileAsByteArray { get; set; }
    }
}
