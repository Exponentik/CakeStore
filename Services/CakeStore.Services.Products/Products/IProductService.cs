using System.Runtime.InteropServices;

namespace CakeStore.Services.Products;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAll();
    Task<ProductModel> GetById(Guid id);
    Task<ProductModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id);
}
