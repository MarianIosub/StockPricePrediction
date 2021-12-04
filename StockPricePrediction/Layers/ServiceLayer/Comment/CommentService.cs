using System.Collections.Generic;
using RepositoryLayer;
using DomainLayer;


namespace ServiceLayer
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private IStockRepository _stockRepository;

        public CommentService(ICommentRepository repository,IStockRepository stockRepository)
        {
            _repository = repository;
            _stockRepository = stockRepository;
        }
        public IEnumerable<Comment> GetAllComments()
        {
            throw new System.NotImplementedException();
        }

        public Comment GetComment(int id)
        {
            return _repository.Get(id);
        }

        public void InsertComment(Comment comment,int stockId)
        { 
            _stockRepository.AddComment(comment,stockId);
            _repository.Insert(comment);
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
    }
}