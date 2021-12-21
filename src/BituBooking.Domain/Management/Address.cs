namespace BituBooking.Domain.Management;

using System.Collections.Generic;
using System.Text.Json.Serialization;

using BituBooking.SharedKernell.Domain;

public class Address : ValueObject
{
    public static Address None => new("", "", "", "", 0);

    private Address() : this("", "", "", "", 0) { }

    [JsonConstructor]
    public Address(string street, string district, string city, string country, int zipCode)
    {
        // TODO It is needed to implement a validation result here instead of throw new exception

        Street = street;
        District = district;
        City = city;
        Country = country;
        ZipCode = zipCode;
    }

    public string Street { get; protected set; }
    public string District { get; protected set; }
    public string City { get; protected set; }
    public string Country { get; protected set; }
    public int ZipCode { get; protected set; }

    public static Address Create(string street, string district, string city, string country, int zipCode)
    {
        return new Address(street, district, city, country, zipCode);
    }

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Street;
        yield return District;
        yield return City;
        yield return Country;
        yield return ZipCode;
    }

    public override string ToString()
    {
        return $"{Street}{Environment.NewLine}," +
               $"{District}{Environment.NewLine}," +
               $"{City}{Environment.NewLine}," +
               $"{Country}{Environment.NewLine}," +
               $"{ZipCode}";
    }

}
