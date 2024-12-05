
using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class AppointmentState
    {
        protected AppointmentState() { }

        public AppointmentState(DateTime created)
        {
            Created = created;
        }

        public int Id { get; set; }

        // FK
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public DateTime Created { get; set; }

        [StringLength(255, ErrorMessage = "Type should be at least 255")]
        public string? Type { get; set; }

        //// ???
        //public AppointmentState(DateTime created)
        //{
        //    Created = created;
        //}

        //public AppointmentState()
        //{
        //    Created = DateTime.UtcNow;
        //}
    }
}