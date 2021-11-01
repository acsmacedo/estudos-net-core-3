using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Entities;

namespace Estudos.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task<int> AddAsync(InboundAddPost data);
        Task UpdateAsync(int id, InboundUpdatePost data);
        Task DeleteByIdAsync(int id);  
    }
}
