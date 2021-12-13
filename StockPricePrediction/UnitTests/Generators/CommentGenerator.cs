using System;
using System.Collections.Generic;
using DomainLayer;

namespace UnitTests.Generators
{
    public class CommentGenerator : IGenerator<Comment>
    {
        public IEnumerable<Comment> GenerateEnum(int count)
        {
            var comments = new List<Comment>();
            for (var i = 1; i <= count; i++)
            {
                comments.Add(new Comment()
                {
                    Id = i,
                    Author = "ABC",
                    Message = "ABC",
                    CreationDate = DateTime.Now,
                    Likes = 5,
                    Dislikes = 3
                });
            }

            return comments;
        }
    }
}