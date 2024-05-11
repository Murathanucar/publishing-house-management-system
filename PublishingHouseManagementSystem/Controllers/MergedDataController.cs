using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace PublishingHouseManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MergedDataController : ControllerBase
    {

        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public MergedDataController(IPublisherRepository publisherRepository, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public OkObjectResult GetAllData()
        {
            var publishers = _publisherRepository.GetAllPublishers();
            var authors = _authorRepository.GetAllAuthors();
            var books = _bookRepository.GetAllBooks();

            var result = new
            {
                publishers = publishers.Select(publisher => new
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    Books = books.Where(book => book.PublisherId == publisher.Id).Select(book => new
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Price = book.Price,
                        ISBN = book.ISBN,
                        PublisherId = book.PublisherId,
                        Author = authors.Where(author => author.BookId == book.Id).Select(author => new
                        {
                            Id = author.Id,
                            FirsName = author.FirstName,
                            LastName = author.LastName,
                            BookId = author.BookId,
                        })
                    })
                })
            };

            return Ok(result);
        }
    }
}
