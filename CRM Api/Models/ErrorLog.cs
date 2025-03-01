namespace CRM_Api.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public DateTime ErrorTime { get; set; } = DateTime.UtcNow;
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
