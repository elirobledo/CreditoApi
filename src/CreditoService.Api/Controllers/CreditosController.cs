using CreditoService.Api.Models;
using CreditoService.Api.Repositories;
using CreditoService.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreditoService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditosController : ControllerBase
    {
            private readonly ICreditoService _service;
            public CreditosController(ICreditoService service)
            {
                _service = service;
            }
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Credito>>> GetAll()
            {
                var items = await _service.GetAllAsync();
                return Ok(items);
            }
            [HttpGet("{id:long}")]
            public async Task<ActionResult<Credito>> Get(long id)
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null) return NotFound();
                return Ok(item);
            }

        [HttpPost]
        public async Task<ActionResult<Credito>> Post([FromBody] Credito credito)
        {
            var created = await _service.CreateAsync(credito);
            return CreatedAtAction(nameof(Get), new { id = created.Id },
            created);
        }

    }
}
