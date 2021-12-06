using System.Collections.Generic;
using DomainLayer;

namespace RepositoryLayer
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetAll(Stock stock);
        Comment Get(int id);
        void Insert(Comment entity);
        void Update(Comment entity);
        void Delete(Comment entity);
        void Remove(Comment entity);
        public void UpVote(Comment entity);
        public void DownVote(Comment entity);
        void SaveChanges();
    }
}