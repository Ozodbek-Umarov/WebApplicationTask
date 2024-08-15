using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTask.Application.DTOs.CategoryDtos;
using WebApplicationTask.Application.Services;

namespace WebApplicationTask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddCategoryDto categoryDto)
    {
        await _categoryService.CreateAsync(categoryDto);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] AddCategoryDto categoryDto)
    {
        await _categoryService.UpdateAsync(id, categoryDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok();
    }
}
