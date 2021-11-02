using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Interfaces.Repositories;
using Estudos.Interfaces.Services;

namespace Estudos.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _repository;

        public PostsService(IPostsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OutboundPost>> GetAsync(InboundSearchPosts search)
        {
            var entities = await _repository.GetAsync(search);

            var result = entities.Select(x => new OutboundPost(x));

            return result;
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            await _repository.DeleteAsync(ids);
        }

        public async Task<IEnumerable<OutboundPost>> AddAsync(
            IEnumerable<InboundAddPost> data)
        {
            var ids = await _repository.AddAsync(data);

            var result = await GetAsync(new InboundSearchPosts(ids));
            
            return result;
        }
        
        public async Task<IEnumerable<OutboundPost>> UpdateAsync(
            IEnumerable<InboundUpdatePost> data)
        {
            await _repository.UpdateAsync(data);

            var result = await GetAsync(new InboundSearchPosts(data.Select(x => x.Id)));

            return result;
        }
    }
}
