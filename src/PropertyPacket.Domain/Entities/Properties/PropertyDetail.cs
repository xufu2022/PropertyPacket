namespace PropertyTenants.Domain.Entities.Properties;

public class PropertyDetail
{
    //public PropertyDetail(Guid id) => Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
    public Guid Id { get; init; }
    public required string Description { get; set; }
    public int MaxGuests { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public bool HasWifi { get; set; }
    public bool HasKitchen { get; set; }
    public string[] Photos { get; set; } = [];
    public Property? Property { get; set; } 
}