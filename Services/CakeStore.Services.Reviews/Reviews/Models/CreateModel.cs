using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;
using CakeStore.Settings;
using FluentValidation;

namespace CakeStore.Services.Reviews;

public class CreateModel
{
    public Guid ProductId { get; set; }

    public Guid UserId { get; set; }
    public string Comment { get; set; }

}



public class CreateBookModelValidator : AbstractValidator<CreateModel>
{
    public CreateBookModelValidator(IDbContextFactory<MainDbContext> contextFactory)
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

        RuleFor(x => x.Comment)
            .MaximumLength(10000).WithMessage("Maximum length is 1000");
    }
}