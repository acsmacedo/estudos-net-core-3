using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] InboundSearchPosts data)
        {
            var result = await _service.GetAsync(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _service.GetAsync(new InboundSearchPosts(id));
            return Ok(result.FirstOrDefault());
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAsync(InboundAddPost data)
        {
            var result = await _service.AddAsync(new[] { data });
            return Ok(result.FirstOrDefault());
        }

        [HttpPost]
        [Route("records")]
        public async Task<IActionResult> AddAsync(IEnumerable<InboundAddPost> data)
        {
            var result = await _service.AddAsync(data);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, InboundUpdatePost data)
        {
            var result = await _service.UpdateAsync(new[] { data });
            return Ok(result.FirstOrDefault());
        }

        [HttpPut]
        [Route("records")]
        public async Task<IActionResult> UpdateAsync(IEnumerable<InboundUpdatePost> data)
        {
            var result = await _service.UpdateAsync(data);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            await _service.DeleteAsync(null);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(new[] { id });
            return Ok();
        }

        [HttpDelete]
        [Route("records")]
        public async Task<IActionResult> DeleteAsync(InboundDeletePosts data)
        {
            await _service.DeleteAsync(data.Records);
            return Ok();
        }
    }
}
