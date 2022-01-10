namespace DomainLayer
{
    public class Likes
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }

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