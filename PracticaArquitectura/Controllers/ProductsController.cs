using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PracticaArquitectura.Models.DTOs.Requests;
using PracticaArquitectura.Services.Implementations;

namespace PracticaArquitectura.Controllers
{
    public class ProductsController : Controller
    {
        private ProductService _service = new ProductService();

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null) 
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpPost]
        public IActionResult Create(ProductForCreateDto dto) 
        {
            var createdProduct = _service.CreateProduct(dto);
            
            return CreatedAtAction(nameof(GetById), new {id = createdProduct.Id});
        }
        [HttpPut("{id}")] 
        public IActionResult Update(int id, ProductForUpdateDto dto)
        {
            var existingProduct = _service.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound(); 
            }

            _service.UpdateProduct(id, dto);
            return NoContent();
        }
        [HttpDelete("{id}")] 
        public IActionResult Delete(int id)
        {
            var existingProduct = _service.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound(); 
            }

            _service.DeleteProduct(id);
            return NoContent();
        }
        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            var resultados = _service.SearchProductsByName(name);
            return Ok(resultados);
        }
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var estadisticas = _service.GetStats();
            return Ok(estadisticas);
        }
    }
}
