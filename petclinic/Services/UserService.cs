using petclinic.Abstractionss;
using petclinic.Contexts;
using petclinic.Models;

namespace petclinic.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseContext _db;
        private readonly ILogger<UserService> _logger;
        public UserService(IDatabaseContext db, ILogger<UserService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Reservation> ReserveAppointment(Reservation reservation)
        {
            return await _db.ReserveAppointment(reservation);
        }

        public async Task<User> UpdatePasswordAsync(User user)
        {
            return await _db.UpdatePasswordAsync(user);
        }

        public async Task<User> Register(User user)
        {
            return await _db.Register(user);
        }

        public async Task<List<Appointment>> GetAvailableAppointments()
        {

            return await _db.GetAvailableAppointments();
        }

        public async Task<User> Login(User user)
        {

            return await _db.Login(user);
        }
    }
}
