using System;

namespace CRM_WPF.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}
