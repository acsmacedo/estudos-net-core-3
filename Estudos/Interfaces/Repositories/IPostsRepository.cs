using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Entities;

namespace Estudos.Interfaces.Repositories
{
    public interface IPostsRepositoryCommand
    {
        Task DeleteAsync(IEnumerable<int> ids); 
        Task UpdateAsync(IEnumerable<InboundUpdatePost> data);
        Task<IEnumerable<int>> AddAsync(IEnumerable<InboundAddPost> data);
    }

    public interface IPostsRepositoryQuery
    {
        Task<IEnumerable<Post>> GetAsync(InboundSearchPosts search);
    }

    public interface IPostsRepository : IPostsRepositoryCommand, IPostsRepositoryQuery
    {
    }
}
