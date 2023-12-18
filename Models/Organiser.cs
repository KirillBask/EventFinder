namespace EventFinder.Models
{
    public class Organiser
    {
        public Guid Id { get; set; }
        public string? OrganiserName { get; set; }
        public List<Event>? Events { get; set; }
    }
}
