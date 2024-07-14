// Models/Product.cs
using System;

namespace InventoryService.Models;

public class Product
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
