using Estudos.Entities;

namespace Estudos.DTO
{
    public class OutboundPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public OutboundPost(Post post)
        {
            Id = post.Id;
            UserId = post.UserId;
            Title = post.Title;
            Body = post.Body;
        }
    }
}
