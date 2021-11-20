using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using DomainLayer;

namespace StockPricePrediction.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Property

        private readonly IUserService _UserService;

        #endregion

        #region Constructor

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        #endregion

        [HttpGet(nameof(GetUser))]
        public IActionResult GetUser([FromQuery(Name = "id")] int id)
        {
            var result = _UserService.GetUser(id);
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }

        [HttpGet(nameof(GetAllUsers))]
        public IActionResult GetAllUsers()
        {
            var result = _UserService.GetAllUsers();
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }

        [HttpPost(nameof(InsertUser))]
        public IActionResult InsertUser(User User)
        {
            _UserService.InsertUser(User);
            return Ok("Data inserted");
        }

        [HttpPut(nameof(UpdateUser))]
        public IActionResult UpdateUser(User User)
        {
            _UserService.UpdateUser(User);
            return Ok("Update done");
        }

        [HttpDelete(nameof(DeleteUser))]
        public IActionResult DeleteUser(int Id)
        {
            _UserService.DeleteUser(Id);
            return Ok("Data Deleted");
        }
    }
}