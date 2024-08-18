namespace CakeStore.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using CakeStore.Services.Products;
using CakeStore.Services.Logger;
using CakeStore.Services.Products;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IProductService bookService;

    public ProductController(IAppLogger logger, IProductService bookService)
    {
        this.logger = logger;
        this.bookService = bookService;
    }

    [HttpGet("")]
    public async Task<IEnumerable<ProductModel>> GetAll()
    {
        var result = await bookService.GetAll();

        return result;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await bookService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<ProductModel> Create(CreateModel request)
    {
        var result = await bookService.Create(request);

        return result;
    }

    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await bookService.Update(id, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await bookService.Delete(id);
    }

}
