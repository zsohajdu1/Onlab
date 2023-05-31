using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.DTO;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("")]
        public string getUserId()
        {
            return "\"" + User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() + "\"";
        }

        [HttpGet]
        [Route("{id}")]
        public string getUserName(string id)
        {
            return "\"" + _userService.getUserName(id) + "\"";
        }
    }
}