using ArgoPantry.Domain.Models.JoinTables;

namespace ArgoPantry.Domain.Models.Entities;

public class Order : DomainObject {
    public string OrderDate { get; set; } = string.Empty;

    // Navigation properties
    public Student Student { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}