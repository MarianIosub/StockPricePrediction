using System;
using System.Collections;
using System.Collections.Generic;
using RepositoryLayer;
using DomainLayer;
using ServiceLayer.Models;


namespace ServiceLayer
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IStockRepository _stockRepository;

        public CommentService(ICommentRepository repository, IStockRepository stockRepository)
        {
            _repository = repository;
            _stockRepository = stockRepository;
        }

        public ApiResponse<IEnumerable<Comment>> GetAllComments(string symbol)
        {
            var stock = _stockRepository.GetBySymbol(symbol);
            return stock == null
                ? ApiResponse<IEnumerable<Comment>>.Fail("Stocks Empty")
                : ApiResponse<IEnumerable<Comment>>.Success(_repository.GetAll(stock));
        }

        public ApiResponse<Comment> GetComment(int id)
        {
            if (id < 0)
                return ApiResponse<Comment>.Fail("Invalid id");
            var comment = _repository.Get(id);
            return comment != null
                ? ApiResponse<Comment>.Success(comment)
                : ApiResponse<Comment>.Fail("Comment does not exist");
        }


        public ApiResponse<int> InsertComment(string author, string message, string stockSymbol, DateTime date)
        {
            var comment = new Comment(author, message)
            {
                CreationDate = date
            };
            if (author == null || message == null || stockSymbol == null)
            {
                return ApiResponse<int>.Fail("Invalid comment");
            }

            _repository.Insert(comment);
            return ApiResponse<int>.Success(_stockRepository.AddComment(comment, stockSymbol));
        }

        public ApiResponse<bool> UpdateComment(Comment comment)
        {
            if (comment == null)
                return ApiResponse<bool>.Fail("Null comment");
            _repository.Update(comment);
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> DeleteComment(int id)
        {
            if (id < 0)
            {
                return ApiResponse<bool>.Fail("Invalid id");
            }

            var comment = GetComment(id).Data;
            _repository.Remove(comment);
            _repository.SaveChanges();
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> Upvote(int id)
        {
            var comment = GetComment(id);
            return comment == null
                ? ApiResponse<bool>.Fail("Null Comment cannot be upvoted")
                : ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> Downvote(int id)
        {
            var comment = GetComment(id);
            if (comment.Data == null)
            {
                return ApiResponse<bool>.Fail("Comment doesn't exist");
            }

            _repository.DownVote(comment.Data);
            return ApiResponse<bool>.Success(true);
        }
    }
}