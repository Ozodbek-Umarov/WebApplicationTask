using FluentValidation;
using WebApplicationTask.Application.DTOs.ProductDtos;

namespace WebApplicationTask.Application.Common.Validators;

public class ProductValidator : AbstractValidator<AddProductDto>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(p => p.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
    }
}
