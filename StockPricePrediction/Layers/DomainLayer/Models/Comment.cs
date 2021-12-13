using System;

namespace DomainLayer
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public Comment(string author, string message)
        {
            Author = author;
            Message = message;
        }

        public Comment(string author, string message, DateTime creationDate)
        {
            Author = author;
            Message = message;
            CreationDate = creationDate;
        }

        public Comment()
        {
        }
    }
}