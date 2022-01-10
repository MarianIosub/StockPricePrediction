using System;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Http;
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

        private const int InternalServerError = 500;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                return StatusCode(InternalServerError);
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
                return StatusCode(InternalServerError);
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }

            return BadRequest("Invalid password or email");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut(nameof(UpdateUser))]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var token = header.Parameter;
                var response = _userService.ValidateUser(token).Data;
                if (response is null)
                {
                    return Unauthorized();
                }


                _userService.UpdateUser(user);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }

            return Ok("User updated");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete(nameof(DeleteUser))]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var token = header.Parameter;
                var response = _userService.ValidateUser(token).Data;
                if (response is null)
                {
                    return Unauthorized();
                }


                _userService.DeleteUser(id);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }

            return Ok("Data Deleted");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(nameof(AddFavouriteStock))]
        public IActionResult AddFavouriteStock([FromBody] FavouriteStockModel favouriteStockModel)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var token = header.Parameter;
                var response = _userService.ValidateUser(token).Data;
                if (response is null)
                {
                    return Unauthorized();
                }


                var id = (int) response;
                var user = _userService.GetUser(id).Data;
                _userService.AddFavouriteStock(user, favouriteStockModel.StockSymbol);
                return Ok(response);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(nameof(RemoveFavouriteStock))]
        public IActionResult RemoveFavouriteStock([FromBody] FavouriteStockModel favouriteStockModel)
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var token = header.Parameter;
                var response = _userService.ValidateUser(token).Data;
                if (response is null)
                {
                    return Unauthorized();
                }


                var id = (int) response;
                var user = _userService.GetUser(id).Data;
                _userService.RemoveFavouriteStock(user, favouriteStockModel.StockSymbol);
                return Ok(response);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet(nameof(GetFavouriteStocks))]
        public IActionResult GetFavouriteStocks()
        {
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var token = header.Parameter;
                var response = _userService.ValidateUser(token).Data;
                if (response is null)
                {
                    return Unauthorized();
                }


                var id = (int) response;
                var user = _userService.GetUser(id).Data;
                var favouriteStocks = _userService.GetFavouriteStocks(user).Data;
                return Ok(favouriteStocks);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }
        }
    }
}