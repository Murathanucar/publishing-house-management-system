using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PublishingHouseManagementSystem.Controllers;

namespace PublishingHouseManagementSystem.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void GetBookById_WithExistingId_ReturnsOkObjectResult()
        {
            int existingId = 1;
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBookById(existingId)).Returns(new Book { Id = existingId, Title = "Sample Book" });
            var controller = new BookController(mockBookRepository.Object);

            var result = controller.GetBookById(existingId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(existingId, book.Id);
        }

        [Fact]
        public void GetBookById_WithNonExistingId_ReturnsNotFoundResult()
        {
            int nonExistingId = 100;
            var mockBookRepository = new Mock<IBookRepository>();
            mockBookRepository.Setup(repo => repo.GetBookById(nonExistingId)).Returns((Book)null);
            var controller = new BookController(mockBookRepository.Object);

            var result = controller.GetBookById(nonExistingId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}