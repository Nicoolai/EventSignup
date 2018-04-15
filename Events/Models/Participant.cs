namespace Events.Models
{
    class Participant : IParticipant
    {
        public string Name { get; set; }
        public IAddress Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool isValid() => (!string.IsNullOrEmpty(Name) && Address != null && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Email));
    }
}
