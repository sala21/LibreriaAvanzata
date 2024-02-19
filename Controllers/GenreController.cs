using WebAppLibreria.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppLibreria.Services;
using Microsoft.EntityFrameworkCore;

namespace WebAppLibreria.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly LibraryDB _context;
    public GenreController(LibraryDB context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreItem>>> GetGenres()
    {
        var genres = await _context.Genres.ToListAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreItem>> GetGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }
        return genre;
    }

    [HttpPost]
    public async Task<ActionResult<GenreItem>> CreateGenre(GenreItem genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreItem genre)
    {
        if (id != genre.Id)
        {
            return BadRequest();
        }

        _context.Entry(genre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenreExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        // Verifica se ci sono libri associati a questo genere
        var booksWithGenre = await _context.Books.AnyAsync(b => b.IdGen == id);
        if (booksWithGenre)
        {
            return BadRequest("Impossibile eliminare il genere poiché ci sono libri associati ad esso.");
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExists(int id)
    {
        return _context.Genres.Any(e => e.Id == id);
    }

}

