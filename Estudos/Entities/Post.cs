namespace Estudos.Entities
{
    public class Post
    {
        public int UserId { get; }
        public int Id { get; set; }
        public string Title { get; }
        public string Body { get; }

        public Post(
            int userId, 
            int id, 
            string title, 
            string body)
        {
            UserId = userId;
            Id = id;
            Title = title;
            Body = body;
        }
    }
}
