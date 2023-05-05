using petclinic.Models;

namespace petclinic.Abstractions
{
    public interface IClinicAdminService
    {
        Task<string> DeleteUserAsync(string username);
        Task<Appointment> AddAppointment(Appointment appointment);
        Task<string> DeleteAppointmentAsync(string dateTime);
        Task<User> Login(User user);
    }
}