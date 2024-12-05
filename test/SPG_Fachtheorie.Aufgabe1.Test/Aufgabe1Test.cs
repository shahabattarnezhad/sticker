using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class Aufgabe1Test
    {
        /// <summary>
        /// Generates database in C:\Scratch\SPG_Fachtheorie.Aufgabe1.Test\Debug\net6.0\appointments.db
        /// </summary>
        private AppointmentContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=appointments.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
            // Arrange Act Assert
            Assert.True(db.Appointments.Count() == 0);
        }

        [Fact]
        public void AddPatientSuccessTest()
        {
            // Arrange
            using var db = GetEmptyDbContext();

            var patient = new Patient
                ("Nick",
                "Anderson",
                "nick@gmail.com",
                "1234556",
                new Address(123, "Queen", "Vienna"));

            // Act
            db.Patients.Add(patient);
            db.SaveChanges();
            db.ChangeTracker.Clear();

            // Assert
            Assert.True(db.Patients.First().FirstName == "Nick");
            Assert.True(db.Patients.First().LastName == "Anderson");
            Assert.True(db.Patients.First().Email == "nick@gmail.com");
        }

        [Fact]
        public void AddAppointmentSuccessTest()
        {
            // Arrange
            using var db = GetEmptyDbContext();

            var appointment = new Appointment
                (
                    new DateTime(2024, 12, 03),
                    new TimeSpan(10, 00, 00),
                    new DateTime(2024, 11,29),
                    new Patient(
                        "Albert",
                        "Einstein",
                        "albert@gmail.com",
                        "1234",
                        new Address(123, "street1", "Vienna")),
                    new AppointmentState(new DateTime(2024, 11, 29))
                );

            // Act
            db.Appointments.Add(appointment);
            db.SaveChanges();
            db.ChangeTracker.Clear();

            Assert.True(db.Appointments.Any());
            Assert.True(db.AppointmentStates.Any());
        }

        [Fact]
        public void ChangeAppointmentStateToConfirmedSuccessTest()
        {
            // Arrange
            using var db = GetEmptyDbContext();

            var appointment = new Appointment
                (
                    new DateTime(2024, 12, 03),
                    new TimeSpan(10, 00, 00),
                    new DateTime(2024, 11, 29),
                    new Patient(
                        "Albert",
                        "Einstein",
                        "albert@gmail.com",
                        "1234",
                        new Address(123, "street1", "Vienna")),
                    new AppointmentState(new DateTime(2024, 11, 29))
                );

            //Act
            db.Appointments.Add(appointment);
            db.SaveChanges();
            db.ChangeTracker.Clear();

            var appointmentFromDb = 
                db.Appointments.Include(a => a.AppointmentState)
              .FirstOrDefault(a => a.Id == appointment.Id);

            appointmentFromDb!.AppointmentState = new ConfirmedAppointmentState
                (TimeSpan.FromHours(1),"Accept", DateTime.Now);

            db.SaveChanges();
            db.ChangeTracker.Clear();

            var count = db.AppointmentStates.Count();

            Assert.True(db.AppointmentStates.Any());
            Assert.Equal(1, count);
        }
    }
}