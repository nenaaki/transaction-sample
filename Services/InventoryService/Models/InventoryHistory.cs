using System;

namespace InventoryService.Models;

public record InventoryHistory
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public Ulid ProductId { get; set; }
    public int QuantityChange { get; set; }
    public DateTime ChangeDate { get; set; }
}
