namespace CakeStore.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeStore.Services.Images;
using CakeStore.Services.Logger;
using CakeStore.Common.Security;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Image")]
[Route("v{version:apiVersion}/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IImageService imageService;

    public ImageController(IAppLogger logger, IImageService imageService)
    {
        this.logger = logger;
        this.imageService = imageService;
    }
   // [Authorize(AppScopes.BooksRead)]
    [HttpGet("")]
    public async Task<IEnumerable<ImageModel>> GetAll()
    {
        var result = await imageService.GetAll();

        return result;
    }
    //[Authorize(AppScopes.BooksRead)]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await imageService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpGet("user/{userId:Guid}")]

    //[Authorize(AppScopes.BooksWrite)]
    [HttpPost("")]
    public async Task<ImageModel> Create(CreateModel request)
    {
        var result = await imageService.Create(request);

        return result;
    }
    //[Authorize(AppScopes.BooksWrite)]
    //[Authorize(AppScopes.BooksWrite)]
    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await imageService.Delete(id);
    }

}
