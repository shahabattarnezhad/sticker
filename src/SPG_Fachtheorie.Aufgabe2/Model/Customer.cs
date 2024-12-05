using SPG_Fachtheorie.Aufgabe2.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class Customer : Entity<int>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected Customer()
        { }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Customer(string firstname, string lastname, string email, DateTime registrationDate)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            RegistrationDate = registrationDate;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public List<Sticker> Stickers { get; set; } = new List<Sticker>();


        public CustomerDto ToDto()
        {
            //List<VehicleDto> vehicles = _vehicles
            //    .Select(v => v.ToDto())
            //    .ToList();

            //List<StickerDto> stickers = _sticker
            //    .Select(s => s.ToDto())
            //    .ToList();

            return new CustomerDto(Id, Guid, Firstname, Lastname, Email, RegistrationDate);
            //return new CustomerDto(Guid, Firstname, Lastname, Email, RegistrationDate, vehicles, stickers);
        }
    }
}