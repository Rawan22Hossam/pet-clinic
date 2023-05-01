using Contexts;
using Models;
using petclinic.Models;
using StudentAPI.Services;

namespace petclinic.Services
{
    public class UserService
    {
        private readonly IDatabaseContext _db;
        private readonly ILogger<UserService> _logger;
        public UserService(IDatabaseContext db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task ReserveAppointments(Reservation reservation)
        {
            var result = await _db.ReserveAppointments();

            return result;
        }

        public async Task UpdatePasswordAsync(User user)
        {
            var result = await _db.UpdatePasswordAsync();

            return result;
        }

        public async Task<User> Register(User user)
        {

            await _db.Register();

            return ;
        }

        public async Task<List<Appointment>> GetAvailableAppointments()
        {
            await _db.GetAvailableAppointments();

            return appointments;
        }

        public async Task<User> Login(User user)
        {

            await _db.Login(user);

            return null;
        }
    }
}
