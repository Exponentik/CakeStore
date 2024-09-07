using CakeStore.Services.Images;
using System.Runtime.InteropServices;

namespace CakeStore.Services.Images;

public interface IImageService
{
    Task<IEnumerable<ImageModel>> GetAll();
    Task<ImageModel> GetById(Guid id);
    Task Update(Guid id, UpdateModel model);
    Task<ImageModel> Create(CreateModel model);
    Task Delete(Guid id);
}
