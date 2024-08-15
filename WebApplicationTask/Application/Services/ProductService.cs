using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplicationTask.Application.Common.Exceptions;
using WebApplicationTask.Application.DTOs.ProductDtos;
using WebApplicationTask.Data.Interfaces;

namespace WebApplicationTask.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AddProductDto> _productValidator;
    private readonly IFileService _fileService;

    public ProductService(IProductRepository productRepository, IMapper mapper, IValidator<AddProductDto> productValidator, IFileService fileService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _productValidator = productValidator;
        _fileService = fileService;
    }

    public async Task<ProductDto> CreateAsync(AddProductDto productDto)
    {
        var validationResult = await _productValidator.ValidateAsync(productDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = _mapper.Map<Data.Entities.Product>(productDto);

        if (productDto.Image != null)
        {
            product.ImagePath = _fileService.UploadImage(productDto.Image);
        }

        await _productRepository.CreateAsync(product);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product not found");
        }

        await _productRepository.DeleteAsync(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync()
            .Include(p => p.Category)
            .ToListAsync();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product not found");
        }

        product = await _productRepository.GetAllAsync()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task UpdateAsync(int id, AddProductDto dto)
    {
        var validationResult = await _productValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Product not found");
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.CategoryId = dto.CategoryId; 

        await _productRepository.UpdateAsync(product);
    }
}
