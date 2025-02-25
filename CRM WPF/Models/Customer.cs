using System;

namespace CRM_WPF.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastPurchaseDate { get; set; }
        public string Notes { get; set; } = "";
        public string AdditionalInfo { get; set; } = "";
    }
}
