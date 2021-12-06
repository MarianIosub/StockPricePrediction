using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<Comment> _entities;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<Comment>();
        }
        public IEnumerable<Comment> GetAll(Stock stock)
        {
            return _entities.AsEnumerable();
        }

        public Comment Get(int id)
        {
            return _entities.SingleOrDefault(c => c.Id == id);
        }

        public void Insert(Comment entity)
        {
            if(entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Update(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
            _appDbContext.SaveChanges();
        }

        public void Delete(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public void Remove(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
        public void Upvote(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Likes += 1;
            _entities.Update(entity);
            _appDbContext.SaveChanges();
        }
        public void Downvote(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Dislikes += 1;
            _entities.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}