namespace EventFinder.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Photos { get; set; }
        public List<Event>? Events { get; set; }
    }
}
