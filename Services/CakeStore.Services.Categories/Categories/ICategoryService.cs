using System.Runtime.InteropServices;

namespace CakeStore.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryModel>> GetAll();
    Task<CategoryModel> GetById(Guid id);
    Task<CategoryModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id);
}
