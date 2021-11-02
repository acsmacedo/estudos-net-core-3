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

        public async Task<IEnumerable<OutboundPost>> GetAllAsync()
        {
            var entities =  await _repository.GetAsync(null);

            return entities.Select(x => new OutboundPost(x));
        }

        public async Task<IEnumerable<OutboundPost>> GetByIdAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.GetAsync(ids);

            return entities.Select(x => new OutboundPost(x));
        }

        public async Task<OutboundPost> GetByIdAsync(int id)
        {
            var entities = await _repository.GetAsync(new[] { id });

            var result = entities.Select(x => new OutboundPost(x));

            return result.FirstOrDefault();
        }

        public async Task DeleteAllAsync()
        {
            await _repository.DeleteAsync(null);
        }

        public async Task DeleteByIdAsync(IEnumerable<int> ids)
        {
            await _repository.DeleteAsync(ids);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteAsync(new[] { id });
        }

        public async Task<IEnumerable<OutboundPost>> AddAsync(
            IEnumerable<InboundAddPost> data)
        {
            return await AddInternalAsync(data);
        }
        
        public async Task<OutboundPost> AddAsync(InboundAddPost data)
        {
            var result = await AddInternalAsync(new[] { data });

            return result.FirstOrDefault();
        }

        private async Task<IEnumerable<OutboundPost>> AddInternalAsync(
            IEnumerable<InboundAddPost> data)
        {
            var ids = await _repository.AddAsync(data);

            var result = new List<OutboundPost>();
            foreach (var id in ids)
            {
                result.Add(await GetByIdAsync(id));
            }
            
            return result;
        }

        public async Task<IEnumerable<OutboundPost>> UpdateAsync(
            IEnumerable<InboundUpdatePost> data)
        {
            return await UpdateInternalAsync(data);
        }

        public async Task<OutboundPost> UpdateAsync(InboundUpdatePost data)
        {
            var result = await UpdateInternalAsync(new[] { data });

            return result.FirstOrDefault();
        }

        private async Task<IEnumerable<OutboundPost>> UpdateInternalAsync(
            IEnumerable<InboundUpdatePost> data)
        {

            await _repository.UpdateAsync(data);

            var result = new List<OutboundPost>();
            foreach (var item in data)
            {
                result.Add(await GetByIdAsync(item.Id));
            }
            
            return result;
        }
    }
}
