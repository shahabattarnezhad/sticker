using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System.Threading;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        //public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentState> AppointmentStates => Set<AppointmentState>();
        public DbSet<ConfirmedAppointmentState> ConfirmedAppointmentStates => Set<ConfirmedAppointmentState>();
        public DbSet<DeletedAppointmentState> DeletedAppointmentStates => Set<DeletedAppointmentState>();

        public AppointmentContext()
        { }

        public AppointmentContext(DbContextOptions options)
                    : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=appointments.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                        .OwnsOne(p => p.Address);

            modelBuilder.Entity<AppointmentState>()
                        .HasDiscriminator<string>("Type")
                        .HasValue<ConfirmedAppointmentState>("Confirm")
                        .HasValue<DeletedAppointmentState>("Delete");
        }
    }
}