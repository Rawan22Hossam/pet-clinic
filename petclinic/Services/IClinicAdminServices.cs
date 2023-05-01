using Models;
using petclinic.Models;

namespace Abstractions
{
    public interface IClinicAdminServices
    {
        Task<bool> GetUserAvailability();
        Task DeleteUserAsync(User user);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task DeleteAppointmentsAsync(Appointment appointment);
        Task<User> Login(User user);
    }
}