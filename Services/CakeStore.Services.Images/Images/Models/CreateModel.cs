using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;
using CakeStore.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CakeStore.Services.Images;

public class CreateModel
{

    public Guid ProductId { get; set; }

    public IFormFile ImageData { get; set; }

}



public class CreateImageModelValidator : AbstractValidator<CreateModel>
{
    public CreateImageModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        //RuleFor(x => x.Name).ProductTitle();

        //RuleFor(x => x.UserId)
        //    .NotEmpty().WithMessage("Author is required")
        //    .Must((id) =>
        //    {
        //        using var context = contextFactory.CreateDbContext();
        //        var found = context.Users.Any(a => a.Id == id);
        //        return found;
        //    }).WithMessage("Author not found");
    }
}