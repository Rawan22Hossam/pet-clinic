using Microsoft.AspNetCore.Mvc;
using petclinic.Abstractionss;
using petclinic.Models;
using petclinic.Services;

namespace petclinic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<User> Register(User user)
        {
            var result = await _service.Register(user);
            return result;
        }
        [HttpPut]
        public async Task<User> UpdatePasswordAsync(User user)
        {
            return await _service.UpdatePasswordAsync(user);
        }

        [HttpPost]
        public async Task<Reservation> ReserveAppointment(Reservation reservation)
        {
            return await _service.ReserveAppointment(reservation); 
        }

        [HttpPost]
        public async Task<string> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(user);
                if (result != null)
                    return "Login succedded to " + result.username;
            }
            return "Failed";
        }

        [HttpGet]
        public async Task<List<Appointment>> GetAvailableAppointments()
        {
            var appointments = await _service.GetAvailableAppointments();
            return appointments;
        }
    }
}