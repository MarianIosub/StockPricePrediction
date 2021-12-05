using System.Collections.Generic;
using DomainLayer;

namespace ServiceLayer
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments(string symbol);
        Comment GetComment(int id);
        void InsertComment(Comment comment,int stockId);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
        
    }
}