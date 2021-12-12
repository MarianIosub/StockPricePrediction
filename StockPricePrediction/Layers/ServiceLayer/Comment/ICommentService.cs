using System;
using System.Collections;
using System.Collections.Generic;
using DomainLayer;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public interface ICommentService
    {
        ApiResponse<IEnumerable<Comment>> GetAllComments(string symbol);
        ApiResponse<Comment> GetComment(int id);
        ApiResponse<int> InsertComment(string author, string comment, string stockSymbol, DateTime date);
        ApiResponse<bool> UpdateComment(Comment comment);
        ApiResponse<bool> DeleteComment(int id);
        ApiResponse<bool> Upvote(int id);
        ApiResponse<bool> Downvote(int id);
    }
}