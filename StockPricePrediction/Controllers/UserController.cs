using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public IActionResult RegisterUser(User user)
        {
            if (_userService.InsertUser(user))
            {
                return Created("User created", user);
            }

            return BadRequest("Invalid password or email");
        }

        [HttpPut(nameof(UpdateUser))]
        public IActionResult UpdateUser(User user)
        {
            _userService.UpdateUser(user);
            return Ok("Update done");
        }

        [HttpDelete(nameof(DeleteUser))]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok("Data Deleted");
        }

        [HttpPost(nameof(LoginUser))]
        public IActionResult LoginUser([FromBody] AuthenticateModel givenUser)
        {
            var response = _userService.Authenticate(givenUser);
            if (response == null)
            {
                return Unauthorized("{}");
            }

            return Ok(response);
        }

        // [HttpPost(nameof(AddFavouriteStock))]
        // public IActionResult AddFavouriteStock([FromBody] string token, [FromBody] int userId,
        //     [FromBody] int stockId)
        // {
        //     var response = _userService.ValidateUser(token);
        //     if (response == null)
        //     {
        //         return Unauthorized("{}");
        //     }
        //
        //     var user = _userService.GetUser(userId);
        //     _userService.AddFavouriteStock(user, stockId);
        //
        //     return Ok(response);
        // }
        //
        // [HttpPost(nameof(RemoveFavouriteStock))]
        // public IActionResult RemoveFavouriteStock([FromBody] string token, [FromBody] int userId,
        //     [FromBody] int stockId)
        // {
        //     var response = _userService.ValidateUser(token);
        //     if (response == null)
        //     {
        //         return Unauthorized("{}");
        //     }
        //
        //     var user = _userService.GetUser(userId);
        //     _userService.RemoveFavouriteStock(user, stockId);
        //     return Ok(response);
        // }
    }
}