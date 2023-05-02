using petclinic.Models;

namespace petclinic.Contexts
{
    public interface IDatabaseContext
    {
        Task<List<Appointment>> GetAvailableAppointments();
        Task<User> Register(User user);
        Task UpdatePasswordAsync(User user);
        Task DeleteUserAsync(User user);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task DeleteAppointmentAsync(Appointment appointment);
        Task ReserveAppointment(Reservation reservation);
        Task<User> Login(User user);
    }
}
