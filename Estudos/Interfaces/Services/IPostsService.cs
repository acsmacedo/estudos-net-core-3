using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;

namespace Estudos.Interfaces.Services
{
    public interface IPostsServiceCommand
    {
        Task DeleteAllAsync();
        Task DeleteByIdAsync(IEnumerable<int> ids);
        Task DeleteByIdAsync(int id);
        Task<OutboundPost> AddAsync(InboundAddPost data);
        Task<IEnumerable<OutboundPost>> AddAsync(IEnumerable<InboundAddPost> data);
        Task<OutboundPost> UpdateAsync(InboundUpdatePost data);
        Task<IEnumerable<OutboundPost>> UpdateAsync(IEnumerable<InboundUpdatePost> data);
    }

    public interface IPostsServiceQuery
    {
        Task<IEnumerable<OutboundPost>> GetAllAsync();
        Task<IEnumerable<OutboundPost>> GetByIdAsync(IEnumerable<int> ids);
        Task<OutboundPost> GetByIdAsync(int id);
    }

    public interface IPostsService : IPostsServiceCommand, IPostsServiceQuery
    {
    }
}
