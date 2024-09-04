using AutoMapper;
using FluentValidation;
using CakeStore.Settings;
using CakeStore.Context.Entities;

namespace CakeStore.Services.Categories;

public class UpdateModel
{
    public string Title { get; set; }
}



public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {

        RuleFor(x => x.Title)
            .MaximumLength(100000).WithMessage("Maximum length is 1000");
    }
}