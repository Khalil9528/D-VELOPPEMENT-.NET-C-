using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceProject.Data;
using WebServiceProject.Intefraces;
using WebServiceProject.Models;
using WebServiceProject.Repository;

namespace WebServiceProject.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _BookRepository;
        private readonly DataContext _context;
        public BookController(IBookRepository BookRepository, DataContext context)
        {
            _BookRepository = BookRepository;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]

        public IActionResult GetBooks()
        {
            var Books = _BookRepository.GetBooks();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Books);
        }
        /*
        [HttpGet("{BookId}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]

        public IActionResult GetBook(int id)
        {
            var Book = _BookRepository.GetBookById(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Book);
        }
        */
        [HttpGet("{BookTitle}")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]

        public IActionResult GetMoGetBookByTitle(string BookTitle)
        {
            var Book = _BookRepository.GetBookByTitle(BookTitle);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Book);

        }
        

     
    }
}
