using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.Application.Common.Exceptions;
using WebApplicationTask.Application.DTOs.CategoryDtos;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository,
                             IMapper mapper,
                             IValidator<AddCategoryDto> categoryValidator) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddCategoryDto> _categoryValidator = categoryValidator;


    public async Task<CategoryDto> CreateAsync(AddCategoryDto categoryDto)
    {
        var validationResult = await _categoryValidator.ValidateAsync(categoryDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var category = _mapper.Map<Data.Entities.Category>(categoryDto);
        await _categoryRepository.CreateAsync(category);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Category not found");
        }
        await _categoryRepository.DeleteAsync(category);
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync().ToListAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Category not found");
        }
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task UpdateAsync(int id, AddCategoryDto dto)
    {
        var validationResult = await _categoryValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Category not found");
        }
        category.Name = dto.Name;
        await _categoryRepository.UpdateAsync(category);
    }
}
