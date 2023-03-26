using BeMo.Models;
using BeMo.Models.DTOs.Requests;
using BeMo.Models.DTOs.Responses;
using BeMo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeMo.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest _request)
        {
            var user = await _userRepository.GetByPropertyAsync(x => x.Username == _request.username);

            if (user is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (!BCrypt.Net.BCrypt.Verify(_request.password, user.Password))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            return new LoginResponse { userId = user.Id };
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterRequest _request)
        {
            if (await _userRepository.ExistsByPropertyAsync( x => x.Username == _request.username))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            User user = new User();
            user.FirstName = _request.firstName;
            user.LastName = _request.lastName;
            user.Username = _request.username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _userRepository.InsertAsync(user);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
