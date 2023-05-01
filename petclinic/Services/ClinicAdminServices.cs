using Abstractions;
using Contexts;
using Microsoft.Extensions.Logging;
using Models;
using petclinic.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace StudentAPI.Services
{
    public class ClinicAdminServices : IClinicAdminServices
    {
        private readonly IDatabaseContext _db;
        private readonly ILogger<ClinicAdminServices> _logger;
        public ClinicAdminServices(IDatabaseContext db, ILogger<ClinicAdminServices> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> GetUserAvailability()
        {
            var result = await _db.GetUserAvailability();

            return result;
        }

        public async Task DeleteUserAsync(User user)
        {
            var result = await _db.DeleteUserAsync();

            return result;
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {

            await _db.AddAppointment(appointment);

            return ;
        }

        public async Task DeleteAppointmentsAsync(Appointment appointment)
        {
            await _db.DeleteAppointmentsAsync(appointment);

            return ;
        }

        public async Task<User> Login(User user)
        {

            await _db.Login(user);

            return null;
        }
    }
}