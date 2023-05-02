using petclinic.Models;

namespace petclinic.Abstractionss
{
    public interface IUserService
    {
        Task ReserveAppointment(Reservation reservation);
        Task UpdatePasswordAsync(User user);
        Task<User> Register(User user);
        Task<List<Appointment>> GetAvailableAppointments();
        Task<User> Login(User user);
    }
}