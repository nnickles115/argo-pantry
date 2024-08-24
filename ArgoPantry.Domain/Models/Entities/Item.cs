using ArgoPantry.Domain.Models.JoinTables;

namespace ArgoPantry.Domain.Models.Entities;

public class Item : DomainObject {
    public string ItemName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;

    // Navigation property
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}