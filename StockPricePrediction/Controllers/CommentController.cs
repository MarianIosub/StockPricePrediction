﻿using System.Web.Http.Cors;
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
            var result = _commentService.GetComment(id);
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }
        [HttpGet(nameof(GetAllComments))]
        public IActionResult GetAllComments()
        {
            var result = _commentService.GetAllComments();
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }
        
        [HttpPost(nameof(InsertComment))]
        public IActionResult InsertComment(Comment comment,int stockId)
        {
            _commentService.InsertComment(comment,stockId);
            return Ok("Data inserted");
        }
    }
}