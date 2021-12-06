using System;
using System.Collections.Generic;
using DomainLayer;

namespace ServiceLayer
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments(string symbol);
        Comment GetComment(int id);
        int InsertComment(string author, string comment, string stockSymbol, DateTime date);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
        void Upvote(int id);
        void Downvote(int id);

    }
}