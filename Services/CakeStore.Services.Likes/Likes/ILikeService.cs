using System.Runtime.InteropServices;

namespace CakeStore.Services.Likes;

public interface ILikeService
{
    Task<IEnumerable<LikeModel>> GetAll();
    Task<LikeModel> GetById(Guid id);
    Task<LikeModel> GetByUserId(Guid id);
    Task<LikeModel> GetByProductIdAndUserId(Guid productId, Guid userId);
    Task<LikeModel> Create(CreateModel model);
    Task Delete(Guid id);
}
