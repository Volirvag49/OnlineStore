using System;

namespace Store.ApiStore.VewModels.Image
{
    public class ImagePostModel
    {
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime LastModified { get; set; }
        public string FileAsBase64 { get; set; }
    }
}
