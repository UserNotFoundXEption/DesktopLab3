using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Shared;

namespace FilmDiary;

public interface IProductService
{
    Task<ObservableCollection<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
}