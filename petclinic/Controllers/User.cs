using Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using petclinic.Services;

namespace Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class User : ControllerBase
    {
        private readonly UserService _service;

        public User(UserService service)
        {
            _service = service;
        }

       
    }
}