namespace CakeStore.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using CakeStore.Services.Categories;
using CakeStore.Services.Logger;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Category")]
[Route("v{version:apiVersion}/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly ICategoryService caregoryService;

    public CategoryController(IAppLogger logger, ICategoryService caregoryService)
    {
        this.logger = logger;
        this.caregoryService = caregoryService;
    }
   // [Authorize(AppScopes.BooksRead)]
    [HttpGet("")]
    public async Task<IEnumerable<CategoryModel>> GetAll()
    {
        var result = await caregoryService.GetAll();

        return result;
    }
    //[Authorize(AppScopes.BooksRead)]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await caregoryService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
   //[Authorize(AppScopes.BooksWrite)]
    [HttpPost("")]
    public async Task<CategoryModel> Create(CreateModel request)
    {
        var result = await caregoryService.Create(request);

        return result;
    }
    //[Authorize(AppScopes.BooksWrite)]
    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await caregoryService.Update(id, request);
    }
    //[Authorize(AppScopes.BooksWrite)]
    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await caregoryService.Delete(id);
    }

}
