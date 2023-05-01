using Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace petclinic.Controllers
{
    public class CliniAdmin
    {
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class User : ControllerBase
        {
            private readonly IClinicAdminServices _service;

            public User(IClinicAdminServices service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<bool> GetUserAvailability()
            {
                var result = await _service.GetUserAvailability();
                return Ok(result);
            }

            [HttpDelete]
            public async Task DeleteUserAsync(User user)
            {
                // need to apply data security by user and language Code
                var result = await _service.DeleteUserAsync();
                return Ok(result);
            }
            [HttpDelete]
            public async Task DeleteAppointmentsAsync(Appointment appointment)
            {
                // need to apply data security by user and language Code
                var result = await _service.DeleteAppointmentsAsync(appointment);
                return Ok(result);
            }

            [HttpPost]
            public async Task<Appointment> AddAppointment(Appointment appointment)
            {
                if (ModelState.IsValid)
                {
                    var result = await _service.AddAppointment(appointments);
                    return Ok(result);
                }
                return Ok("Error!!");
            }

           
        }
    }
}
