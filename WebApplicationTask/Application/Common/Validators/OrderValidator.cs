using FluentValidation;
using WebApplicationTask.Application.DTOs.OrderDtos;

namespace WebApplicationTask.Application.Common.Validators;

public class OrderValidator : AbstractValidator<CreateOrderDto>
{
    public OrderValidator()
    {
        RuleFor(o => o.Count)
            .GreaterThan(0).WithMessage("Count is required.");

        RuleFor(o => o.ProductId)
            .GreaterThan(0).WithMessage("Product ID is required.");
    }
}
