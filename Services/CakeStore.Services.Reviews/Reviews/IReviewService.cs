using System.Runtime.InteropServices;

namespace CakeStore.Services.Reviews;

public interface IReviewService
{
    Task<IEnumerable<ReviewModel>> GetAll();
    Task<ReviewModel> GetById(Guid id);
    Task<ReviewModel> Create(CreateModel model);
    Task Update(Guid id, UpdateModel model);
    Task Delete(Guid id);
}
