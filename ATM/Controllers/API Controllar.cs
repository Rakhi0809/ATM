using ATM.DTO;
using ATM.Repository;
using ATM.RTO;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_Controllar : ControllerBase
    {
        private readonly IUserRepository UserRepository;
        public API_Controllar(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await UserRepository.GetallUserAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = await UserRepository.CreateUserAsync(userDto);
            return Ok( user);
        }
    }

}
