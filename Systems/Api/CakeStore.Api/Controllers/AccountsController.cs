namespace CakeStore.API.Controllers;

using AutoMapper;
using CakeStore.Services.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using CakeStore.Services.Products;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Product")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AccountsController> logger;
    private readonly IUserAccountService userAccountService;

    public AccountsController(IMapper mapper, ILogger<AccountsController> logger, IUserAccountService userAccountService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.userAccountService = userAccountService;
    }
    [HttpGet("")]
    public async Task<IEnumerable<UserAccountModel>> GetAll()
    {
        var result = await userAccountService.GetAll();

        return result;
    }
    //[Authorize(AppScopes.BooksRead)]
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await userAccountService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
    [HttpPost("")]
    public async Task<UserAccountModel> Register([FromQuery] RegisterUserAccountModel request)
    {
        var user = await userAccountService.Create(request);
        return user;
    }
}
    