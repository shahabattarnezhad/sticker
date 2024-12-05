
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    /// <summary>
    /// Der Termin des Patienten.
    /// </summary>
    public class Appointment
    {
        protected Appointment() { }

        public Appointment
            (DateTime date,
            TimeSpan time,
            DateTime created,
            Patient patient,
            AppointmentState appointmentState)
        {
            Date = date;
            Time = time;
            Created = created;
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            AppointmentState = appointmentState ?? throw new ArgumentNullException(nameof(appointmentState));
        }


        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Created { get; set; }
        
        // FK
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public AppointmentState? AppointmentState { get; set; }

        //protected Appointment()
        //{
        //    Patient = null!;
        //    AppointmentState = null!;
        //}
    }
}