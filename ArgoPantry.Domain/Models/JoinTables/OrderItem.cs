using ArgoPantry.Domain.Models.Entities;

namespace ArgoPantry.Domain.Models.JoinTables;

public class OrderItem {
    public int QuantityTaken { get; set; } = 0;

    // Composite Key properties
    public int OrderId { get; set; }
    public int ItemId { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
    public Item Item { get; set; } = null!;
}