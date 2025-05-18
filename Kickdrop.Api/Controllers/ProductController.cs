using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kickdrop.Api.Models;
using Kickdrop.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kickdrop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Shoe>>> GetAllShoes()
        {
            var shoes = await _productService.GetAllShoesAsync();
            return Ok(shoes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Shoe>> GetShoeById(int id)
        {
            var shoe = await _productService.GetShoeByIdAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }
            return Ok(shoe);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Shoe>> CreateShoe(Shoe shoe)
        {
            var createdShoe = await _productService.CreateShoeAsync(shoe);
            return CreatedAtAction(nameof(GetShoeById), new { id = createdShoe.Id }, createdShoe);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateShoe(int id, Shoe shoe)
        {
            if (id != shoe.Id)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateShoeAsync(shoe);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            var result = await _productService.DeleteShoeAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("color/{color}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Shoe>>> GetShoesByColor(ShoeColor color)
        {
            var shoes = await _productService.GetShoesByColorAsync(color);
            return Ok(shoes);
        }
    }
}