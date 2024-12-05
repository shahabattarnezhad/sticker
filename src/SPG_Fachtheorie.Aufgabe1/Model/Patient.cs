using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Patient
    {
        protected Patient() { }

        public Patient
            (string firstName,
            string lastName,
            string email,
            string phone,
            Address address)
        {
           FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "First name should be at least 255")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Last name should be at least 255")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Email should be at least 255")]
        public string Email { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Phone should be at least 255")]
        public string Phone { get; set; } = string.Empty;

        public Address? Address { get; set; }

        //[Required]
        //[StringLength(maximumLength: 255, ErrorMessage = "FirstName is too long")]
        //public string? FirstName { get; set; } = string.Empty;

        //[Required]
        //[StringLength(maximumLength: 255, ErrorMessage = "LastName is too long")]
        //public string? LastName { get; set; } = string.Empty;

        //public Address? Address { get; set; }

        //[Required]
        //[StringLength(maximumLength: 255, ErrorMessage = "Email is too long")]
        //public string? Email { get; set; } = default;

        //[Required]
        //[StringLength(maximumLength: 255, ErrorMessage = "Phone is too long")]
        //public string? Phone { get; set; } = default;
    }
}