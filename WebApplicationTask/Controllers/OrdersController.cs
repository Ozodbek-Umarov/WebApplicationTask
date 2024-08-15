using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTask.Application.Common.Exceptions;
using WebApplicationTask.Application.DTOs.OrderDtos;
using WebApplicationTask.Application.Services;

namespace WebApplicationTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderDto orderDto)
    {
        var createdOrder = await _orderService.CreateAsync(orderDto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(order);
        }
        catch (StatusCodeException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreateOrderDto orderDto)
    {
        try
        {
            await _orderService.UpdateAsync(id, orderDto);
            return Ok();
        }
        catch (StatusCodeException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _orderService.DeleteAsync(id);
            return Ok(); 
        }
        catch (StatusCodeException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}