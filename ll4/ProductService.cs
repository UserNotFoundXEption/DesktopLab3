using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Shared;

namespace FilmDiary;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ObservableCollection<Product>> GetProductsAsync()
    {
        try
        {
            var films = await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("api/films");
            return new ObservableCollection<Product>(films ?? Enumerable.Empty<Product>());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.GetFilmsAsync: {ex.Message}");
            return new ObservableCollection<Product>();
        }
    }

    public async Task AddProductAsync(Product film)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/films", film);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.AddFilmAsync: {ex.Message}");
        }
    }

    public async Task UpdateProductAsync(Product film)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/films/{film.Name}", film);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.UpdateFilmAsync: {ex.Message}");
        }
    }

    public async Task DeleteProductAsync(Product film)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/films/{film.Name}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FilmService.DeleteFilmAsync: {ex.Message}");
        }
    }
}