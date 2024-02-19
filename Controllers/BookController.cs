using WebAppLibreria.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppLibreria.Services;
using Microsoft.EntityFrameworkCore;


namespace WebAppLibreria.Controller;


[ApiController]
[Route("[controller]")]

public class BookController : ControllerBase
{
    private readonly LibraryDB _context;
    public BookController(LibraryDB context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookItem>>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookItem>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound(); 
        }
        return book;
    }

    [HttpPost]
    public async Task<ActionResult<BookItem>> CreateBook(BookItem book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, BookItem book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
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
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}


