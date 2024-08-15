using System.Net.Http;
using System.Net.Http.Json;
using WpfAppTask.Data.Entities;

namespace WpfAppTask.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7146/api/products") };
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>("products");
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>($"products/{id}");
    }

    public async Task CreateProductAsync(Product product)
    {
        await _httpClient.PostAsJsonAsync("products", product);
    }

    public async Task UpdateProductAsync(int id, Product product)
    {
        await _httpClient.PutAsJsonAsync($"products/{id}", product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _httpClient.DeleteAsync($"products/{id}");
    }
}
