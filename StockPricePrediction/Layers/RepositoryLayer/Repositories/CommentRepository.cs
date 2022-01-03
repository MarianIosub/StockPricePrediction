using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Exceptions;

namespace RepositoryLayer
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Comment> _entities;
        private readonly DbSet<Likes> _likes;
        private readonly DbSet<Dislikes> _dislikes;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<Comment>();
            _likes = _appDbContext.Set<Likes>();
            _dislikes = _appDbContext.Set<Dislikes>();
        }

        public IEnumerable<Comment> GetAll(Stock stock)
        {
            return stock.Comments;
        }

        public Comment Get(int id)
        {
            return _entities.SingleOrDefault(c => c.Id == id);
        }

        public void Insert(Comment entity)
        {
            if (entity == null)
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

        public void UpVote(Comment entity, User user)
        {
            if (entity == null || user == null)
            {
                throw new ResultNotFoundException();
            }

            var like = new Likes(user, entity);
            _entities.Include(e => e.UserLikes).Load();
            if (entity.UserLikes.SingleOrDefault(u => u.User == user) == null)
            {
                _likes.Add(like);
                entity.UserLikes.Add(like);
                entity.Likes += 1;
                _entities.Update(entity);
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new NotSupportedException("User already liked this comment!");
            }
        }

        public void DownVote(Comment entity, User user)
        {
            Console.WriteLine(entity.Author);
            if (entity == null || user == null)
            {
                throw new ResultNotFoundException();
            }

            var dislike = new Dislikes(user, entity);
            _entities.Include(e => e.UserDislikes).Load();
            if (entity.UserDislikes.SingleOrDefault(u => u.User == user) == null)
            {
                _dislikes.Add(dislike);
                entity.UserDislikes.Add(dislike);
                entity.Dislikes += 1;
                _entities.Update(entity);
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new NotSupportedException("User already disliked this comment!");
            }
        }
    }
}