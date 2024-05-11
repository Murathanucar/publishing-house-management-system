using DataAccessLayer.Model;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace PublishingHouseManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublisherController : ControllerBase
    {

        private readonly IPublisherRepository _publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [HttpGet]
        public IActionResult GetAllPublishers()
        {
            var publishers = _publisherRepository.GetAllPublishers();
            return Ok(publishers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherRepository.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        // HTTP 201 Created state
        [HttpPost]
        public IActionResult AddPublisher([FromBody] Publisher publisher)
        {
            _publisherRepository.AddPublisher(publisher);
            return CreatedAtAction(nameof(GetPublisherById), new { id = publisher.Id }, publisher);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePublisher(int id, [FromBody] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return BadRequest();
            }

            _publisherRepository.UpdatePublisher(publisher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePublisher(int id)
        {
            _publisherRepository.DeletePublisher(id);
            return NoContent();
        }
    }
}
