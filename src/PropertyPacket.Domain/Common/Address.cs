namespace PropertyTenants.Domain.Common;

public record Address(int Id, string Line1, string Line2, string City, string Country, string PostCode)
{}

public record ContactInfo
{
    private ContactInfo()
    {
    }

    public ContactInfo(string Email, string PhoneNumber, string Mobile, Address Address)
    {
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Mobile = Mobile;
        this.Address = Address;
    }

    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Mobile { get; init; }
    public Address Address { get; init; }


    public void Deconstruct(out string Email, out string PhoneNumber, out string Mobile, out Address Address)
    {
        Email = this.Email;
        PhoneNumber = this.PhoneNumber;
        Mobile = this.Mobile;
        Address = this.Address;
    }
}