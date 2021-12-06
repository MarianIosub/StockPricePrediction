using System;
using System.Collections.Generic;
using RepositoryLayer;
using DomainLayer;


namespace ServiceLayer
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private IStockRepository _stockRepository;

        public CommentService(ICommentRepository repository, IStockRepository stockRepository)
        {
            _repository = repository;
            _stockRepository = stockRepository;
        }

        public IEnumerable<Comment> GetAllComments(string symbol)
        {
            var stock = _stockRepository.GetBySymbol(symbol);
            return _repository.GetAll(stock);
        }

        public Comment GetComment(int id)
        {
            return _repository.Get(id);
        }


        public int InsertComment(string author, string message, string stockSymbol, DateTime date)
        {
            var comment = new Comment(author, message);
            comment.CreationDate = date;
            _repository.Insert(comment);
            return _stockRepository.AddComment(comment, stockSymbol);
        }

        public void UpdateComment(Comment comment)
        {
            _repository.Update(comment);
        }

        public void DeleteComment(int id)
        {
            var comment = GetComment(id);
            _repository.Remove(comment);
            _repository.SaveChanges();
        }

        public void Upvote(int id)
        {
            var comment = GetComment(id);
            _repository.UpVote(comment);
        }

        public void Downvote(int id)
        {
            var comment = GetComment(id);
            _repository.DownVote(comment);
        }
    }
}