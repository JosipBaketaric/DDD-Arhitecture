namespace Logistics.Domain.Import
{
    public class Terminal
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Name { get; private set; }
        public Terminal(string country, string city, string postalCode, string name)
        {
            Country = country;
            City = city;
            PostalCode = postalCode;
            Name = name;
        }
    }
}
