using System;
using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace StockPricePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommentController : ControllerBase
    {
        #region Property

        private readonly ICommentService _commentService;

        #endregion

        #region Constructor

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
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
        public IActionResult AddComment(Comment comment, int stockId)
        {
            try
            {
                _commentService.InsertComment(comment, stockId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }

            return Ok("Data inserted");
        }
    }
}