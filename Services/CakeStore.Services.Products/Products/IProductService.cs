using System.Runtime.InteropServices;

namespace CakeStore.Services.Products.Products;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetAll();
    Task<ProductModel> GetById(Guid id);
    Task<ProductModel> Create(CreateModel model);
    Task<ProductModel> Update(Guid id, UpdateModel model);
    Task<ProductModel> Delete(Guid id);
}
