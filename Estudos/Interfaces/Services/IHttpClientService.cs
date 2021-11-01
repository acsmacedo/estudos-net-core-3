using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;

namespace Estudos.Interfaces.Services
{
    public interface IHttpClientService
    {
        Task<IEnumerable<OutboundPost>> GetAllPostsAsync();
        Task<OutboundPost> GetPostByIdAsync(int id);
        Task<OutboundPost> AddPostAsync(InboundAddPost data);
        Task<OutboundPost> UpdatePostAsync(int id, InboundUpdatePost data);
        Task DeletePostByIdAsync(int id);
    }
}
