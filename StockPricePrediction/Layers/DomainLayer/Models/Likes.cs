namespace DomainLayer
{
    public class Likes
    {
        public int Id { get; set; }
        public User User { get;  }
        public Comment Comment { get;  }

        public Likes()
        {
        }

        public Likes(User user, Comment comment)
        {
            User = user;
            Comment = comment;
        }
    }
}