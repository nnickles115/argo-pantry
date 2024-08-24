namespace ArgoPantry.WPF.Models;

// Flattens out the Order and OrderItem (associated Items from the Order)
// entities into a single entity for the DataGrid
public sealed class TransactionEntity : DomainObject {
    public string OrderDate { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public int QuantityTaken { get; set; } = 0;
}