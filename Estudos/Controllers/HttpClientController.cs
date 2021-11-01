using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        private IHttpClientService _service;
        
        public HttpClientController(IHttpClientService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var result = await _service.GetAllPostsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("posts/{id}")]
        public async Task<IActionResult> GetPostByIdAsync(int id)
        {
            var result = await _service.GetPostByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IActionResult> AddPostAsync(InboundAddPost data)
        {
            var result = await _service.AddPostAsync(data);

            return Ok(result);
        }

        [HttpPut]
        [Route("posts/{id}")]
        public async Task<IActionResult> UpdatePostAsync(int id, InboundUpdatePost data)
        {
            var result = await _service.UpdatePostAsync(id, data);

            return Ok(result);
        }

        [HttpDelete]
        [Route("posts/{id}")]
        public async Task<IActionResult> DeletePostByIdAsync(int id)
        {
            await _service.DeletePostByIdAsync(id);

            return Ok();
        }
    }
}
