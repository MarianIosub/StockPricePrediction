using System;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Models;

namespace StockPricePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommentController : ControllerBase
    {
        #region Property

        private const int InternalServerError = 500;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        #endregion

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(nameof(GetComment))]
        public IActionResult GetComment([FromQuery(Name = "id")] int id)
        {
            try
            {
                var result = _commentService.GetComment(id).Data;
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

            return BadRequest("No records found");
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(nameof(GetAllComments))]
        public IActionResult GetAllComments([FromQuery] string symbol)
        {
            try
            {
                var result = _commentService.GetAllComments(symbol).Data;
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

            return BadRequest("No records found");
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost(nameof(AddComment))]
        public IActionResult AddComment([FromBody] AddCommentModel commentModel)
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
                var commentId = _commentService.InsertComment(user.Lastname + " " + user.Firstname,
                    commentModel.Message,
                    commentModel.StockSymbol, commentModel.CreationDate);

                return Ok(commentId.Data);
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut(nameof(Upvote))]
        public IActionResult Upvote(int commentId)
        {
            // var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            // var token = header.Parameter;
            // var response = _userService.ValidateUser(token).Data;
            // if (response is null)
            // {
            //     return Unauthorized();
            // }
            // var id = (int) response;
            // var user = _userService.GetUser(id).Data;
            var user = _userService.GetUser(5).Data;
            Console.WriteLine(user.Email);
            try
            {
                var status = _commentService.Upvote(commentId, user);
                if (status.Error != null)
                {
                    return StatusCode(403);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }

            return Ok("Comment upvoted");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut(nameof(Downvote))]
        public IActionResult Downvote(int commentId)
        {
            // var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            // var token = header.Parameter;
            // var response = _userService.ValidateUser(token).Data;
            // if (response is null)
            // {
            //     return Unauthorized();
            // }
            // var id = (int) response;
            // var user = _userService.GetUser(id).Data;
            var user = _userService.GetUser(5).Data;
            try
            {
                var status = _commentService.Downvote(commentId, user);
                if (status.Error != null)
                {
                    return StatusCode(403);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(InternalServerError);
            }

            return Ok("Comment downvoted");
        }
    }
}