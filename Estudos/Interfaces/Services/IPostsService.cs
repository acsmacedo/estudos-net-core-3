using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;

namespace Estudos.Interfaces.Services
{
    public interface IPostsServiceCommand
    {
        Task DeleteAsync(IEnumerable<int> ids);
        Task<IEnumerable<OutboundPost>> AddAsync(IEnumerable<InboundAddPost> data);
        Task<IEnumerable<OutboundPost>> UpdateAsync(IEnumerable<InboundUpdatePost> data);
    }

    public interface IPostsServiceQuery
    {
        Task<IEnumerable<OutboundPost>> GetAsync(InboundSearchPosts search);
    }

    public interface IPostsService : IPostsServiceCommand, IPostsServiceQuery
    {
    }
}
