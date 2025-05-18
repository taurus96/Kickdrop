using Xunit;
using Moq;
using Kickdrop.Api.Controllers;
using Kickdrop.Api.Models;
using Kickdrop.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kickdrop.Tests.Unit
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllShoes_ReturnsOkWithShoes()
        {
            var shoes = new List<Shoe>
            {
                new Shoe { Id = 1, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 },
                new Shoe { Id = 2, Brand = "Adidas", Size = 11, Color = ShoeColor.Blue, Price = 120 }
            };
            _mockService.Setup(s => s.GetAllShoesAsync()).ReturnsAsync(shoes);

            var result = await _controller.GetAllShoes();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(shoes, okResult.Value);
        }

        [Fact]
        public async Task GetShoeById_ReturnsOkWithShoe()
        {
            var shoe = new Shoe { Id = 1, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 };
            _mockService.Setup(s => s.GetShoeByIdAsync(1)).ReturnsAsync(shoe);

            var result = await _controller.GetShoeById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(shoe, okResult.Value);
        }

        [Fact]
        public async Task GetShoeById_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetShoeByIdAsync(99)).ReturnsAsync((Shoe)null);

            var result = await _controller.GetShoeById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateShoe_ReturnsCreatedAtAction()
        {
            var shoe = new Shoe { Id = 1, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 };
            _mockService.Setup(s => s.CreateShoeAsync(shoe)).ReturnsAsync(shoe);

            var result = await _controller.CreateShoe(shoe);

            var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(shoe, createdAtAction.Value);
        }

        [Fact]
        public async Task UpdateShoe_ReturnsNoContent()
        {
            var shoe = new Shoe { Id = 1, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 };
            _mockService.Setup(s => s.UpdateShoeAsync(shoe)).ReturnsAsync(shoe);

            var result = await _controller.UpdateShoe(1, shoe);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateShoe_ReturnsBadRequest_WhenIdMismatch()
        {
            var shoe = new Shoe { Id = 2, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 };

            var result = await _controller.UpdateShoe(1, shoe);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateShoe_ReturnsNotFound_WhenShoeNotFound()
        {
            var shoe = new Shoe { Id = 1, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 };
            _mockService.Setup(s => s.UpdateShoeAsync(shoe)).ReturnsAsync((Shoe)null);

            var result = await _controller.UpdateShoe(1, shoe);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteShoe_ReturnsNoContent_WhenDeleted()
        {
            _mockService.Setup(s => s.DeleteShoeAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteShoe(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteShoe_ReturnsNotFound_WhenNotFound()
        {
            _mockService.Setup(s => s.DeleteShoeAsync(1)).ReturnsAsync(false);

            var result = await _controller.DeleteShoe(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetShoesByColor_ReturnsOkWithShoes()
        {
            var shoes = new List<Shoe>
            {
                new Shoe { Id = 1, Brand = "Nike", Size = 10, Color = ShoeColor.Black, Price = 100 }
            };
            _mockService.Setup(s => s.GetShoesByColorAsync(ShoeColor.Black)).ReturnsAsync(shoes);

            var result = await _controller.GetShoesByColor(ShoeColor.Black);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(shoes, okResult.Value);
        }
    }
}