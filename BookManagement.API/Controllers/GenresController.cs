using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(GenresService genresService):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        return Ok(await genresService.GetAllGenres());
    }
}