using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;

namespace Estudos.Interfaces.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<OutboundPost>> GetAllAsync();
        Task<OutboundPost> GetByIdAsync(int id);
        Task<OutboundPost> AddAsync(InboundAddPost data);
        Task<OutboundPost> UpdateAsync(int id, InboundUpdatePost data);
        Task DeleteByIdAsync(int id);   
    }
}
