using System;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [HttpGet(nameof(GetUser))]
        public IActionResult GetUser([FromQuery(Name = "id")] int id)
        {
            try
            {
                var result = _userService.GetUser(id).Data;
                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return NoContent();
        }
        [HttpGet(nameof(GetAllUsers))]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = _userService.GetAllUsers().Data;
                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPost(nameof(RegisterUser))]
        public IActionResult RegisterUser(User user)
        {
            try
            {
                if (_userService.Exists(user).Data)
                {
                    return Conflict();
                }

                var status = _userService.InsertUser(user).Data;

                if (status)
                {
                    return Created("User created", user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return BadRequest("Invalid password or email");
        }

        [HttpPut(nameof(UpdateUser))]
        public IActionResult UpdateUser(User user)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token).Data;
            if (response is null)
            {
                return Unauthorized();
            }

            try
            {
                _userService.UpdateUser(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok("User updated");
        }

        [HttpDelete(nameof(DeleteUser))]
        public IActionResult DeleteUser(int id)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token).Data;
            if (response is null)
            {
                return Unauthorized();
            }

            try
            {
                _userService.DeleteUser(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok("Data Deleted");
        }

        [HttpPost(nameof(LoginUser))]
        public IActionResult LoginUser([FromBody] AuthenticateModel givenUser)
        {
            try
            {
                var response = _userService.Authenticate(givenUser).Data;
                if (response is null)
                {
                    return Unauthorized("{}");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost(nameof(AddFavouriteStock))]
        public IActionResult AddFavouriteStock([FromBody] FavouriteStockModel favouriteStockModel)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token).Data;
            if (response is null)
            {
                return Unauthorized();
            }

            try
            {
                var id = (int) response;
                var user = _userService.GetUser(id).Data;
                _userService.AddFavouriteStock(user, favouriteStockModel.StockSymbol);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }


            return Ok(response);
        }

        [HttpPost(nameof(RemoveFavouriteStock))]
        public IActionResult RemoveFavouriteStock([FromBody] FavouriteStockModel favouriteStockModel)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token).Data;
            if (response is null)
            {
                return Unauthorized();
            }

            try
            {
                var id = (int) response;
                var user = _userService.GetUser(id).Data;
                _userService.RemoveFavouriteStock(user, favouriteStockModel.StockSymbol);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok(response);
        }

        [HttpGet(nameof(GetFavouriteStocks))]
        public IActionResult GetFavouriteStocks()
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token).Data;
            if (response is null)
            {
                return Unauthorized();
            }

            try
            {
                var id = (int) response;
                var user = _userService.GetUser(id).Data;
                var favouriteStocks = _userService.GetFavouriteStocks(user).Data;
                return Ok(favouriteStocks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
    }
}