using Models;
using petclinic.Models;
using System.Data.SqlClient;

namespace Contexts
{
    public interface IDatabaseContext
    {
        Task<List<Appointment>> GetAvailableAppointments();
        Task<bool> GetUserAvailability();
        Task<User> Register(User user);
        Task UpdatePasswordAsync(User user);
        Task DeleteUserAsync(User user);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task DeleteAppointmentsAsync(Appointment appointment);
        Task ReserveAppointments(Reservation reservation);
        Task<User> Login(User user);
    }
}
