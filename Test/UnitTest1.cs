using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using Moq;

namespace DataAccessLayer.Tests
{
    public class FilePublisherRepositoryTests
    {
        [Fact]
        public void GetAllPublishers_ReturnsNonEmptyList()
        {
            // Arrange
            var mockPublisherRepository = new Mock<IPublisherRepository>();
            var fakePublishers = new List<Publisher>
            {
                new Publisher { Id = 1, Name = "Publisher 1" },
                new Publisher { Id = 6, Name = "Publisher 2" }
            };
            mockPublisherRepository.Setup(repo => repo.GetAllPublishers()).Returns(fakePublishers);

            // Act
            var result = mockPublisherRepository.Object.GetAllPublishers();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}