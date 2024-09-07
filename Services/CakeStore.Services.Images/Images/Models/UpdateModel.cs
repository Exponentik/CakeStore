using AutoMapper;
using FluentValidation;
using CakeStore.Settings;
using CakeStore.Context.Entities;

namespace CakeStore.Services.Images;

public class UpdateModel
{
    public byte[] ImageData { get; set; }
}



public class UpdateModelValidator : AbstractValidator<UpdateModel>
{
    public UpdateModelValidator()
    {
    }
}