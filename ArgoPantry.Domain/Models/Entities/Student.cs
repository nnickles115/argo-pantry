
namespace ArgoPantry.Domain.Models.Entities;
public class Student : DomainObject {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}