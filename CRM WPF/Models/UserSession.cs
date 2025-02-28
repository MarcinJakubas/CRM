namespace CRM_WPF.Models
{
    public class UserSession
    {
        public static UserSession CurrentUser { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
    }
}