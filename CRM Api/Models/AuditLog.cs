namespace CRM_Api.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string OperationType { get; set; } // INSERT, UPDATE, DELETE
        public DateTime ChangeTime { get; set; } = DateTime.UtcNow;
    }
}
