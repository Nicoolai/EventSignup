namespace Events.Models
{
    public interface IAddress
    {
        string Country { get; set; }
        string State { get; set; }
        string City { get; set; }
        string PostalCode { get; set; }
        string Address { get; set; }
        double Longitude { get; set;}
        double Latitude { get; set; }
    }
}
