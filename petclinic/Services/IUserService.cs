using Models;
using petclinic.Models;

namespace Abstractions
{
    public interface IUserServices
    {
        Task ReserveAppointments(Reservation reservation);
        Task UpdatePasswordAsync(User user);
        Task<User> Register(User user);
        Task<List<Appointment>> GetAvailableAppointments();
        Task<User> Login(User user);
    }
}