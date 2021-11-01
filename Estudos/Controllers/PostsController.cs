using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(InboundAddPost data)
        {
            var result = await _service.AddAsync(data);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, InboundUpdatePost data)
        {
            var result = await _service.UpdateAsync(id, data);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            await _service.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
