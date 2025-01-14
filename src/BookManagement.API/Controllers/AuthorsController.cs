using BookManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(AuthorsService authorsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        return Ok(await authorsService.GetAllAuthors());
    }
}