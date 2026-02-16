using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using FluentValidation;

namespace ApiProjeKampi.WebApi.ValidationRules;

public class ProductValidator : AbstractValidator<CreateProductDto>
{
    public ProductValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Bu alan zorunludur!");
        RuleFor(x=>x.Description).NotEmpty().WithMessage("Bu alan zorunludur!");
    }
}