namespace Events.Models
{
    interface IAddress
    {
        string Country { get; set; }
        string State { get; set; }
        string City { get; set; }
        string PostalCode { get; set; }
        string Address { get; set; }
    }
}
