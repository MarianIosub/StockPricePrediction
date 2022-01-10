using System;
using System.Collections.Generic;
using DomainLayer;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public interface ICommentService
    {
        ApiResponse<IEnumerable<Comment>> GetAllComments(string stockSymbol);
        ApiResponse<Comment> GetComment(int id);
        ApiResponse<int> InsertComment(string author, string comment, string stockSymbol, DateTime date);
        ApiResponse<bool> UpdateComment(Comment comment);
        ApiResponse<bool> DeleteComment(int id);
        ApiResponse<bool> UpVote(int id, User user);
        ApiResponse<bool> DownVote(int id, User user);
    }
}