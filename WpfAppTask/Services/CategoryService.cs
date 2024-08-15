using System.Net.Http;
using System.Net.Http.Json;
using WpfAppTask.Data.Entities;

namespace WpfAppTask.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7146/api/categories") };
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Category>>("categories");
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Category>($"categories/{id}");
    }

    public async Task CreateCategoryAsync(Category category)
    {
        var response = await _httpClient.PostAsJsonAsync("categories", category);
        response.EnsureSuccessStatusCode(); 
    }


    public async Task UpdateCategoryAsync(int id, Category category)
    {
        await _httpClient.PutAsJsonAsync($"categories/{id}", category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _httpClient.DeleteAsync($"categories/{id}");
    }
}
