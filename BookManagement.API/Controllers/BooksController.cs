using BookManagement.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> Books =
        [
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2010 }
        ];

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(Books);
        }

        [HttpPost]
        public ActionResult AddBook([FromBody] Book newBook)
        {
            newBook.Id = Books.Count + 1;
            Books.Add(newBook);
            return CreatedAtAction(nameof(GetAllBooks), newBook);
        }
    }
}
