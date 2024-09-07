namespace CakeStore.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeStore.Services.Likes;
using CakeStore.Services.Logger;
using CakeStore.Common.Security;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Like")]
[Route("v{version:apiVersion}/[controller]")]
public class LikeController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly ILikeService likeService;

    public LikeController(IAppLogger logger, ILikeService likeService)
    {
        this.logger = logger;
        this.likeService = likeService;
    }
   // [Authorize(AppScopes.BooksRead)]
    [HttpGet("")]
    public async Task<IEnumerable<LikeModel>> GetAll()
    {
        var result = await likeService.GetAll();

        return result;
    }
    //[Authorize(AppScopes.BooksRead)]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await likeService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpGet("user/{userId:Guid}")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
    {
        var result = await likeService.GetByUserId(userId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpGet("product/{productId:Guid}/user/{userId:Guid}")]
    public async Task<IActionResult> GetByProductIdAndUserId([FromRoute] Guid productId, [FromRoute] Guid userId)
    {
        var result = await likeService.GetByProductIdAndUserId(productId, userId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    //[Authorize(AppScopes.BooksWrite)]
    [HttpPost("")]
    public async Task<LikeModel> Create(CreateModel request)
    {
        var result = await likeService.Create(request);

        return result;
    }
    //[Authorize(AppScopes.BooksWrite)]
    //[Authorize(AppScopes.BooksWrite)]
    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await likeService.Delete(id);
    }

}
