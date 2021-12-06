using System;

namespace DomainLayer
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }

        private int _likes = 0;
        private int _dislikes = 0;

        public int Likes
        {
            get => _likes;
            set => _likes = value;
        }

        public int Dislikes
        {
            get => _dislikes;
            set => _dislikes = value;
        }

        public Comment(string author, string message)
        {
            Author = author;
            Message = message;
        }
        
        
    }
}