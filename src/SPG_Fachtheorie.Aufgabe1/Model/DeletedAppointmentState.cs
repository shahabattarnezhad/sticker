using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class DeletedAppointmentState : AppointmentState
    {
        protected DeletedAppointmentState() : base() { }

        public DeletedAppointmentState(DateTime created) 
            : base(created)
        {}
    }
}