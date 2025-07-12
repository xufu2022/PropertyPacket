namespace PropertyPacket.Domain.Common;

public record Address(int Id, string Line1, string Line2, string City, string Country, string PostCode)
{}

public record ContactInfo(string Email, string PhoneNumber, string Mobile, Address Address)
{
    public void Deconstruct(out string email, out string phoneNumber, out string Mobile, out Address Address)
    {
        email = this.Email;
        phoneNumber = this.PhoneNumber;
        Mobile = this.Mobile;
        Address = this.Address;
    }
}