namespace EventFinder.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public Guid EventOrganiser { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public string? EventType { get; set; }
        public List<string>? Photos { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? EventStart { get; set;}
        public DateTime? EventEnd { get; set;}
        public List<Guid>? Participants { get; set; }
    }
}
