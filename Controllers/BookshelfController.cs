using WebAppLibreria.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppLibreria.Services;
using Microsoft.EntityFrameworkCore;


namespace WebAppLibreria.Controller;

[ApiController]
[Route("[controller]")]

public class BookshelfController : ControllerBase
{
    private readonly LibraryDB _context;
    public BookshelfController(LibraryDB context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookshelfItem>>> GetBookshelves()
    {
        var bookshelves = await _context.Bookshelves.ToListAsync();
        return Ok(bookshelves);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookshelfItem>> GetBookshelf(int id)
    {
        var bookshelf = await _context.Bookshelves.FindAsync(id);

        if (bookshelf == null)
        {
            return NotFound();
        }
        return bookshelf;
    }

    [HttpPost]
    public async Task<ActionResult<BookshelfItem>> CreateBookshelf(BookshelfItem bookshelf)
    {
        _context.Bookshelves.Add(bookshelf);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookshelf), new { id = bookshelf.Id }, bookshelf);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookshelf(int id, BookshelfItem bookshelf)
    {
        if (id != bookshelf.Id)
        {
            return BadRequest();
        }

        _context.Entry(bookshelf).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookshelfExists(id))
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
    public async Task<IActionResult> DeleteBookshelf(int id)
    {
        var bookshelf = await _context.Bookshelves.FindAsync(id);
        if (bookshelf == null)
        {
            return NotFound();
        }

        // Verifica se ci sono libri associati a questo scaffale
        var booksOnBookshelf = await _context.Books.AnyAsync(b => b.IdShelf == id);
        if (booksOnBookshelf)
        {
            return BadRequest("Impossibile eliminare lo scaffale poiché ci sono libri associati ad esso.");
        }

        _context.Bookshelves.Remove(bookshelf);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private bool BookshelfExists(int id)
    {
        return _context.Bookshelves.Any(e => e.Id == id);
    }

}

