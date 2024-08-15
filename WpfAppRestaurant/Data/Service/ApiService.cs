using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WpfAppRestaurant.Data.Entities;

namespace WpfAppRestaurant.Data.Service;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7146/") // Your API base URL
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Categories");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Category>>(json);
        }
        catch (Exception ex)
        {
            // Handle exceptions properly (log, display error, etc.)
            Console.WriteLine($"Error fetching categories: {ex.Message}");
            return new List<Category>();
        }
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var response = await _httpClient.GetAsync("api/products");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<Product>>(content);

        if (products != null)
        {
            foreach (var product in products)
            {
                product.ImagePath = $"https://localhost:7146/images/{product.ImagePath.TrimStart('~', '/')}";
            }
        }

        return products;
    }


    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await GetProductsFromEndpoint($"api/Products/ByCategory/{categoryId}");
    }

    private async Task<List<Product>> GetProductsFromEndpoint(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            // Assuming your API returns the full image URL
            // If not, adjust the URL construction logic based on your API response
            return products;
        }
        catch (Exception ex)
        {
            // Handle exceptions (log, display error, etc.)
            Console.WriteLine($"Error fetching products from endpoint '{endpoint}': {ex.Message}");
            return new List<Product>();
        }
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7146/api/Product/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Product>(content);
    }

}