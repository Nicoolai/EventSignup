namespace Events.Models
{
    class Participant : IParticipant
    {
        public string Name { get; set; }
        public IAddress Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool IsValid() => (!string.IsNullOrEmpty(this.Name) && this.Address != null && !string.IsNullOrEmpty(this.PhoneNumber) && !string.IsNullOrEmpty(this.Email));
    }
}
