using petclinic.Models;

namespace petclinic.Abstractionss
{
    public interface IUserService
    {
        Task<Reservation> ReserveAppointment(Reservation reservation);
        Task<User> UpdatePasswordAsync(User user);
        Task<User> Register(User user);
        Task<List<Appointment>> GetAvailableAppointments();
        Task<User> Login(User user);
    }
}