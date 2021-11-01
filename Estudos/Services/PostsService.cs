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
        
        public async Task<OutboundPost> AddAsync(InboundAddPost data)
        {
            var entity = await _repository.AddAsync(data);

            return new OutboundPost(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<OutboundPost>> GetAllAsync()
        {
            var entities =  await _repository.GetAllAsync();

            return entities.Select(x => new OutboundPost(x));
        }

        public async Task<OutboundPost> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return new OutboundPost(entity);
        }

        public async Task<OutboundPost> UpdateAsync(int id, InboundUpdatePost data)
        {
            var entity = await _repository.UpdateAsync(id, data);

            return new OutboundPost(entity);
        }
    }
}
