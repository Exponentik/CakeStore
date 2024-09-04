namespace CakeStore.Api.App;

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CakeStore.Services.Reviews;
using CakeStore.Services.Logger;
using CakeStore.Common.Security;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Review")]
[Route("v{version:apiVersion}/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IAppLogger logger;
    private readonly IReviewService reviewService;

    public ReviewController(IAppLogger logger, IReviewService bookService)
    {
        this.logger = logger;
        this.reviewService = bookService;
    }
   // [Authorize(AppScopes.BooksRead)]
    [HttpGet("")]
    public async Task<IEnumerable<ReviewModel>> GetAll()
    {
        var result = await reviewService.GetAll();

        return result;
    }
    //[Authorize(AppScopes.BooksRead)]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await reviewService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
   //[Authorize(AppScopes.BooksWrite)]
    [HttpPost("")]
    public async Task<ReviewModel> Create(CreateModel request)
    {
        var result = await reviewService.Create(request);

        return result;
    }
    //[Authorize(AppScopes.BooksWrite)]
    [HttpPut("{id:Guid}")]
    public async Task Update([FromRoute] Guid id, UpdateModel request)
    {
        await reviewService.Update(id, request);
    }
    //[Authorize(AppScopes.BooksWrite)]
    [HttpDelete("{id:Guid}")]
    public async Task Delete([FromRoute] Guid id)
    {
        await reviewService.Delete(id);
    }

}
