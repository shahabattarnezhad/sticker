using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class ConfirmedAppointmentState : AppointmentState
    {
        protected ConfirmedAppointmentState() : base() { }

        public ConfirmedAppointmentState(TimeSpan duration, string infoText, DateTime created)
            : base(created)
        {
            Duration = duration;
            InfoText = infoText;
        }

        public TimeSpan Duration { get; set; }

        [StringLength(255, ErrorMessage = "Info text should be at least 255")]
        public string? InfoText { get; set; }
    }
}