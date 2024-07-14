using InventoryService.Models;
using System.Threading.Tasks;

namespace InventoryService.Services;

public interface IProductService
{
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(string id, Product updatedProduct);
    Task<Product> GetProductAsync(string id);
    Task<bool> UpdateProductQuantityAsync(string id, int quantityChange);
    Task<bool> CancelQuantityChangeAsync(string historyId);
}
