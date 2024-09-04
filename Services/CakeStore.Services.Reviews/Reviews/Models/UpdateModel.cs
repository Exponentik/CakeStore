using AutoMapper;
using FluentValidation;
using CakeStore.Settings;
using CakeStore.Context.Entities;

namespace CakeStore.Services.Reviews;

public class UpdateModel
{
    public string Comment { get; set; }
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

        RuleFor(x => x.Comment)
            .MaximumLength(10000).WithMessage("Maximum length is 1000");
    }
}