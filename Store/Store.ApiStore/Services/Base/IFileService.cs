using System.Threading.Tasks;
using Store.Database.Entities;

namespace Store.ApiStore.Services.Base
{
    public interface IFileService
    {
        Task UploadImage(Image imageModel);
    }
}
