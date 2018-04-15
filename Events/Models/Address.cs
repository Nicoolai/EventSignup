namespace Events.Models
{
    class Address : IAddress
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        string IAddress.Address { get; set; }
    }
}
