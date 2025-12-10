using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<ProductDto> _products = new()
        {
            new ProductDto { Id = 1, Name = "Kalem", Price = 1.5M, Description = "Mavi uçlu" },
            new ProductDto { Id = 2, Name = "Defter", Price = 12.0M, Description = "80 yaprak" }
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(_products);

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var p = _products.FirstOrDefault(x => x.Id == id);
            return p == null ? NotFound() : Ok(p);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDto dto)
        {
            dto.Id = _products.Any() ? _products.Max(x => x.Id) + 1 : 1;
            _products.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var p = _products.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            _products.Remove(p);
            return NoContent();
        }
    }
}
