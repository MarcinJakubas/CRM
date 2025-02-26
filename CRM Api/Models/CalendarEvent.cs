namespace CRM_Api.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DayOfWeek { get; set; }
        public string EventColor { get; set; }

        public Customer Customer { get; set; }
    }
}