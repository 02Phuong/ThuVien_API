using Microsoft.AspNetCore.Mvc;
using ThuVien_API.Data;
using ThuVien_API.Models.DTO;
using ThuVien_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ThuVien_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : Controller
	{
		private readonly AppDbContext _dbContext;
		public BookController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		//GET http://localhost:port/api/get-all-books
		[HttpGet("get-all-books")]
		public IActionResult GetAll()
		{
			//var allBooksDomain=_dbContext.Book.ToList();
			var allBooksDomain = _dbContext.Book;
			//Map domain models to DTOs
			var allBooksDTO= allBooksDomain.Select(Books=>new BookDTO()
			{
				Id=Books.BookID,
				Title=Books.Title,
				Description=Books.Description,
				IsRead = Books.IsRead,
				DateRead=Books.IsRead? Books.DateRead.Value : null,
				Rate = Books.IsRead ? Books.Rate.Value : null,
				Genre=Books.Genre,
				CoverUrl=Books.CoverUrl,
				PublisherName=Books.Publisher.Name,
				AuthorName=Books.Book_Authors.Select(n=>n.Author.FullName).ToList(),
			}).ToList();
			return Ok(allBooksDomain);
		}

		// GET: api/Students
		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<Books>> GetBooks()
		//{
		//	return await _dbContext.Books.ToListAsync();
		//}

		// GET: api/Students/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Books>> GetBooks(int id)
		{
			var Book = await _dbContext.Book.FindAsync(id);

			if (Book == null)
			{
				return NotFound();
			}

			return Book;
		}
		// POST: api/Students
		[HttpPost]
		public async Task<ActionResult<Books>> PostStudent(Books books)
		{
			_dbContext.Book.Add(books);
			await _dbContext.SaveChangesAsync();

			return CreatedAtAction("GetBooks", new { id = books.BookID }, books);
		}
		// PUT: api/Students/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutStudent(int id, Books book)
		{
			if (id != book.BookID)
			{
				return BadRequest();
			}

			_dbContext.Entry(book).State = EntityState.Modified;

			try
			{
				await _dbContext.SaveChangesAsync();
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
		// DELETE: api/Students/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await _dbContext.Book.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			_dbContext.Book.Remove(book);
			await _dbContext.SaveChangesAsync();

			return NoContent();
		}

		private bool BookExists(int id)
		{
			return _dbContext.Book.Any(e => e.BookID == id);
		}

		//public async Task<IActionResult> GetBooks (int id)
		//{

		//	return;
		//}
	}
}
