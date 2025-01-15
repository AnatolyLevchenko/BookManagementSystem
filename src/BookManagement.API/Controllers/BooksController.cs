using BookManagement.Domain;
using BookManagement.Services;
using BookManagement.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BooksController(BookService bookService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetPagedList(int page, int pageSize, string? searchTerm)
    {
        return Ok(await bookService.GetPagedList(page, pageSize, searchTerm));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookReadDto>> GetById(int id)
    {
        var book = await bookService.GetBookById(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookCreateDto? book)
    {
        if (book == null)
            return BadRequest();

        var createdBook = await bookService.CreateBook(book);
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto? updatedBook)
    {
        if (updatedBook == null)
            return BadRequest();

        await bookService.UpdateBook(id, updatedBook);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await bookService.DeleteBook(id);
        return NoContent();
    }
}