// Services/ProductService.cs
using InventoryService.Models;
using InventoryService.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace InventoryService.Services;

public class ProductService : IProductService
{
    private readonly ProductContext _context;

    public ProductService(ProductContext context) => _context = context;

    public async Task<Product> AddProductAsync(Product product)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            product.IsAvailable = product.Quantity > 0;
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return product;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Product> UpdateProductAsync(string id, Product updatedProduct)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var productId = Ulid.Parse(id);
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return null;

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;
            product.IsAvailable = updatedProduct.Quantity > 0;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return product;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Product> GetProductAsync(string id)
    {
        var productId = Ulid.Parse(id);
        var product = await _context.Products.FindAsync(productId);
        return product;
    }

    public async Task<bool> UpdateProductQuantityAsync(string id, int quantityChange)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var productId = Ulid.Parse(id);
            var product = await _context.Products.FindAsync(productId);

            if (product == null || product.Quantity + quantityChange < 0)
                return false;

            product.Quantity += quantityChange;
            product.IsAvailable = product.Quantity > 0;
            product.UpdatedAt = DateTime.UtcNow;

            var inventoryHistory = new InventoryHistory
            {
                ProductId = product.Id,
                QuantityChange = quantityChange,
                ChangeDate = DateTime.UtcNow
            };

            _context.InventoryHistories.Add(inventoryHistory);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> CancelQuantityChangeAsync(string historyId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var inventoryHistoryId = Ulid.Parse(historyId);
            var inventoryHistory = await _context.InventoryHistories.FindAsync(inventoryHistoryId);

            if (inventoryHistory == null)
                return false;

            var product = await _context.Products.FindAsync(inventoryHistory.ProductId);
            if (product == null)
                return false;

            if (product.Quantity - inventoryHistory.QuantityChange < 0)
                return false;

            product.Quantity -= inventoryHistory.QuantityChange;
            product.IsAvailable = product.Quantity > 0;
            product.UpdatedAt = DateTime.UtcNow;

            _context.InventoryHistories.Remove(inventoryHistory);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
