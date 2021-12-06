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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommentController : ControllerBase
    {
        #region Property

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

        [HttpGet(nameof(GetComment))]
        public IActionResult GetComment([FromQuery(Name = "id")] int id)
        {
            try
            {
                var result = _commentService.GetComment(id);
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

            return BadRequest("No records found");
        }

        [HttpGet(nameof(GetAllComments))]
        public IActionResult GetAllComments([FromQuery] string symbol)
        {
            try
            {
                var result = _commentService.GetAllComments(symbol);
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

            return BadRequest("No records found");
        }

        [HttpPost(nameof(AddComment))]
        public IActionResult AddComment([FromBody] AddCommentModel commentModel)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token);
            if (response is null)
            {
                return Unauthorized();
            }

            try
            {
                var id = response ?? default(int);
                var user = _userService.GetUser(1);
                _commentService.InsertComment(user.Lastname + " " + user.Firstname, commentModel.Message,
                    commentModel.StockSymbol);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok("Data inserted");
        }

        [HttpPut(nameof(Upvote))]
        public IActionResult Upvote(int commentId)
        {
            try
            {
                _commentService.Upvote(commentId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok("Comment upvoted");
        }

        [HttpPut(nameof(Downvote))]
        public IActionResult Downvote(int commentId)
        {
            try
            {
                _commentService.Downvote(commentId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok("Comment downvoted");
        }
    }
}