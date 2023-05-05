using petclinic.Models;

namespace petclinic.Contexts
{
    public interface IDatabaseContext
    {
        Task<List<Appointment>> GetAvailableAppointments();
        Task<User> Register(User user);
        Task<User> UpdatePasswordAsync(User user);
        Task<string> DeleteUserAsync(string username);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task<string> DeleteAppointmentAsync(string dateTime);
        Task<Reservation> ReserveAppointment(Reservation reservation);
        Task<User> Login(User user);
    }
}
