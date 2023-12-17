namespace EventFinder.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Photos { get; set; }
    }
}
