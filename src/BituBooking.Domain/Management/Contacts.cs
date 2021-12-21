namespace BituBooking.Domain.Management;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using BituBooking.SharedKernell.Domain;

public class Contacts : ValueObject
{
    public static Contacts None => new("", "", "");

    private Contacts() : this("", "", "") { }

    [JsonConstructor]
    public Contacts(string phone, string mobile, string email)
    {
        Phone = phone;
        Mobile = mobile;
        Email = email;
    }

    public string Mobile { get; }
    public string Phone { get; }
    public string Email { get; }

    public static Contacts Create(string phone, string mobile, string email)
    {
        return new Contacts(phone, mobile, email);
    }

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Mobile;
        yield return Phone;
        yield return Email;
    }
}
