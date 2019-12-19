using System;
using System.IO;
using System.Threading.Tasks;
using Store.ApiStore.Services.Base;
using Store.Database.Entities;

namespace Store.ApiStore.Services
{
    public class FileService : IFileService
    {

       const string IMAGE_DIRECTORY = "Images";

        public async Task UploadImage(Image imageModel)
        {
            if (imageModel == null)
                return;

            var directory = Directory.GetCurrentDirectory();

            string fileUrl;

            var fileAsBase64String = imageModel.FileAsBase64
                .Substring(imageModel.FileAsBase64.IndexOf(",") + 1);

            var fileAsBase64 = Convert.FromBase64String(fileAsBase64String);

            if (imageModel.FileUrl == null)
                imageModel.FileUrl = Guid.NewGuid().ToString() + GetFileExtension(fileAsBase64String);

            var filePathName = Path.Combine(directory, IMAGE_DIRECTORY, imageModel.FileUrl);

            using (var fs = new FileStream(
                filePathName, FileMode.OpenOrCreate))
            {
                await fs.WriteAsync(fileAsBase64, 0, fileAsBase64.Length);
            }

        }

        private static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return string.Empty;
            }
        }
    }  
}
