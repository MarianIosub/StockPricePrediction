namespace DomainLayer
{
    public class Dislikes
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }

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