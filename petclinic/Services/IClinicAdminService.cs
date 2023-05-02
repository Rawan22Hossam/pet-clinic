using petclinic.Models;

namespace petclinic.Abstractions
{
    public interface IClinicAdminService
    {
        Task DeleteUserAsync(User user);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task DeleteAppointmentAsync(Appointment appointment);
        Task<User> Login(User user);
    }
}