using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using DomainLayer;
using ServiceLayer.Models;

namespace StockPricePrediction.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ControllerBase
    {
        #region Property

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        #endregion

        [HttpGet(nameof(GetUser))]
        public IActionResult GetUser([FromQuery(Name = "id")] int id)
        {
            var result = _userService.GetUser(id);
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }

        [HttpGet(nameof(GetAllUsers))]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }

        [HttpPost(nameof(RegisterUser))]
        public IActionResult RegisterUser(User User)
        {
            if (_userService.InsertUser(User))
            {
                return Created("User created", User);
            }

            return BadRequest("Invalid password or email");
        }

        [HttpPut(nameof(UpdateUser))]
        public IActionResult UpdateUser(User User)
        {
            _userService.UpdateUser(User);
            return Ok("Update done");
        }

        [HttpDelete(nameof(DeleteUser))]
        public IActionResult DeleteUser(int Id)
        {
            _userService.DeleteUser(Id);
            return Ok("Data Deleted");
        }

        [HttpPost(nameof(LoginUser))]
        public UserResponseModel LoginUser([FromBody] AuthenticateModel givenUser)
        {
            var response = _userService.Authenticate(givenUser);
            return response;
        }
    }
}