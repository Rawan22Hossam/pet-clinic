using petclinic.Abstractions;
using petclinic.Contexts;
using petclinic.Models;
namespace petclinic.Services
{
    public class ClinicAdminService : IClinicAdminService
    {
        private readonly IDatabaseContext _db;
        private readonly ILogger<ClinicAdminService> _logger;
        public ClinicAdminService(IDatabaseContext db, ILogger<ClinicAdminService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task DeleteUserAsync(User user)
        {
            await _db.DeleteUserAsync(user);
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {

            return await _db.AddAppointment(appointment);
        }

        public async Task DeleteAppointmentAsync(Appointment appointment)
        {
            await _db.DeleteAppointmentAsync(appointment);
        }

        public async Task<User> Login(User user)
        {

            return await _db.Login(user);
        }
    }
}