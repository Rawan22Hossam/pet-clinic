using Microsoft.AspNetCore.Mvc;
using petclinic.Abstractions;
using petclinic.Models;

namespace petclinic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClinicAdminController : ControllerBase
    {
        private readonly IClinicAdminService _service;

        public ClinicAdminController(IClinicAdminService service)
        {
            _service = service;
        }

        [HttpDelete]
        public async Task DeleteUserAsync(User user)
        {
            await _service.DeleteUserAsync(user);
        }
        [HttpDelete]
        public async Task DeleteAppointmentsAsync(Appointment appointment)
        {

            await _service.DeleteAppointmentAsync(appointment);
        }

        [HttpPost]
        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AddAppointment(appointment);
                return result;
            }
            return null;
        }

        [HttpGet]
        public async Task<User> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(user);
                return result;
            }
            return null;
        }


    }
}
