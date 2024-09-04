using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;
using CakeStore.Settings;
using FluentValidation;

namespace CakeStore.Services.Categories;

public class CreateModel
{
    public string Title { get; set; }

}

public class CreateCategoryModelValidator : AbstractValidator<CreateModel>
{
    public CreateCategoryModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {

        RuleFor(x => x.Title)
            .MaximumLength(100000).WithMessage("Maximum length is 1000");
    }
}