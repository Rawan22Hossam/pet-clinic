using Microsoft.AspNetCore.Mvc;
using petclinic.Models;
using petclinic.Services;

namespace petclinic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
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
        public async Task UpdatePasswordAsync(User user)
        {

            await _service.UpdatePasswordAsync(user);
        }

        [HttpPost]
        public async Task ReserveAppointment(Reservation reservation)
        {
                await _service.ReserveAppointment(reservation); 
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
        [HttpGet]
        public async Task<List<Appointment>> GetAvailableAppointments()
        {
            var appointments = await _service.GetAvailableAppointments();
            return appointments;
        }
    }
}