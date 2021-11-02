using System.Collections.Generic;

namespace Estudos.DTO
{
    public class InboundSearchPosts
    {
        public string Ids { get; set; }
        public string UserIds { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public InboundSearchPosts() { }

        public InboundSearchPosts(IEnumerable<int> id) 
        {
            Ids = string.Join(",", id);
        }

        public InboundSearchPosts(int id) 
        {
            Ids = id.ToString();
        }
    }
}
