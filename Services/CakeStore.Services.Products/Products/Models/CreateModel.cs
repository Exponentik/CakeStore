using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CakeStore.Context.Entities;
using CakeStore.Context;
using CakeStore.Settings;
using FluentValidation;

namespace CakeStore.Services.Products;

public class CreateModel
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<string> Category { get; set; }
}

public class CreateModelProfile : Profile
{
    public CreateModelProfile()
    {
        CreateMap<CreateModel, Product>()
            //.ForMember(dest => dest.User.Id, opt => opt.Ignore())
            .AfterMap<CreateModelActions>();
    }

    public class CreateModelActions : IMappingAction<CreateModel, Product>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateModel source, Product destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var author = db.Users.FirstOrDefault(x => x.Id == source.UserId);

            //destination.User.Id = author.Id;
        }
    }
}

public class CreateBookModelValidator : AbstractValidator<CreateModel>
{
    public CreateBookModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {
        //RuleFor(x => x.Name).ProductTitle();

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Author is required")
            .Must((id) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Users.Any(a => a.Id == id);
                return found;
            }).WithMessage("Author not found");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Maximum length is 1000");
    }
}