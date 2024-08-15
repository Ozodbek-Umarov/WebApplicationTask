using FluentValidation;
using WebApplicationTask.Application.DTOs.CategoryDtos;

namespace WebApplicationTask.Application.Common.Validators;

public class CategoryValidator : AbstractValidator<AddCategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");
    }
}
