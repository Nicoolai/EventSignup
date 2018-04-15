namespace Events.Models
{
    interface IParticipant
    {
        string Name { get; set; }
        IAddress Address { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        bool isValid();
    }
}
