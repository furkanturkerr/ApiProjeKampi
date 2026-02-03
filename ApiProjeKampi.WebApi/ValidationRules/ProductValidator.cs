using ApiProjeKampi.WebApi.Entities;
using FluentValidation;

namespace ApiProjeKampi.WebApi.ValidationRules;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Bu alan zorunludur!");
        RuleFor(x=>x.Description).NotEmpty().WithMessage("Bu alan zorunludur!");
    }
}