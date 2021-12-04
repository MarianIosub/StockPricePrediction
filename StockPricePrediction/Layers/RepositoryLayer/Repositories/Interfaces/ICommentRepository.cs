using System.Collections.Generic;
using DomainLayer;

namespace RepositoryLayer
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll();
        Comment Get(int id);
        void Insert(Comment entity);
        void Update(Comment entity);
        void Delete(Comment entity);
        void Remove(Comment entity);
        void SaveChanges();
    }
}