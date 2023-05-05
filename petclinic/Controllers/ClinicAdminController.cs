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
        public async Task<string> DeleteUserAsync(string username)
        {
            return await _service.DeleteUserAsync(username);
        }
        [HttpDelete]
        public async Task<string> DeleteAppointmentAsync(string dateTime)
        {
            return await _service.DeleteAppointmentAsync(dateTime);
        }

        [HttpPost]
        public async Task<string> AddAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AddAppointment(appointment);
                if (result != null)
                    return "Appointment " + result.when + " added successfully";
            }
            return "Failed";
        }

        [HttpPost]
        public async Task<string> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(user);
                if(result  != null)
                    return "Login succedded to "+result.username;
            }
            return "Failed";
        }


    }
}
