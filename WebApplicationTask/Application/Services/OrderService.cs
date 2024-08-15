using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.Application.Common.Exceptions;
using WebApplicationTask.Application.DTOs.OrderDtos;
using WebApplicationTask.Data.Entities;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Application.Services;

public class OrderService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IValidator<CreateOrderDto> orderValidator) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateOrderDto> _orderValidator = orderValidator;


    public async Task<OrderDto> CreateAsync(CreateOrderDto orderDto)
    {
        var validationResult = await _orderValidator.ValidateAsync(orderDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = await _unitOfWork.ProductRepository.GetByIdAsync(orderDto.ProductId);

        if (product is null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product not found");

        var order = new Order
        {
            Count = orderDto.Count,
            ProductId = orderDto.ProductId,
            Price = product.Price,
            TotalPrice = orderDto.Count * product.Price
        };
        await _unitOfWork.OrderRepository.CreateAsync(order);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Order not found");
        }
        await _unitOfWork.OrderRepository.DeleteAsync(order);
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync()
            .Include(p => p.Product)
            .ToListAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        var order = await _unitOfWork.OrderRepository.GetAllAsync()
            .Include(p => p.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Order not found");
        }
        return _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateAsync(int id, CreateOrderDto dto)
    {
        var validationResult = await _orderValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
        if (order is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Order not found");
        }
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);

        if (product is null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product not found");

        order.Count = dto.Count;
        order.ProductId = dto.ProductId;
        order.Price = product.Price;
        order.TotalPrice = dto.Count * product.Price;
        await _unitOfWork.OrderRepository.UpdateAsync(order);
    }
}
