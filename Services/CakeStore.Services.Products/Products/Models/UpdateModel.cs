using AutoMapper;
using FluentValidation;
using CakeStore.Settings;
using CakeStore.Context.Entities;

namespace CakeStore.Services.Products;

public class UpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateModelProfile : Profile
{
    public UpdateModelProfile()
    {
        CreateMap<UpdateModel, Product>();
    }
}

public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
        //RuleFor(x => x.Title).ProductName();

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");
    }
}