using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Survey_API.Models;
using Survey_API.Services;

namespace Survey_API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            try
            {
                var token = _userService.Authenticate(user.Email, user.Password);
                user = _userService.GetUserByEmail(user.Email);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(new { token, user });
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
            
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                List<User> users = _userService.GetUsers();

                return Ok(users);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(string id)
        {
            try
            {
                User user = _userService.GetUser(id);

                return Ok(user);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User newUser)
        {
            try
            {
                User user = _userService.CreateUser(newUser);

                return Ok(user);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
