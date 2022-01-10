namespace DomainLayer
{
    public class Dislikes
    {
        public int Id { get; set; }
        public User User { get;  }
        public Comment Comment { get; }

        public Dislikes()
        {
        }

        public Dislikes(User user, Comment comment)
        {
            User = user;
            Comment = comment;
        }
    }
}