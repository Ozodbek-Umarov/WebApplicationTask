﻿namespace WebApplicationTask.Application.DTOs.ProductDtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public int Price { get; set; }
    public int Count { get; set; }
    public string Image { get; set; }
}
