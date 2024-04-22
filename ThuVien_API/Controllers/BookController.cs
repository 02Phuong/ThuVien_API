using Microsoft.AspNetCore.Mvc;
using ThuVien_API.Data;
using ThuVien_API.Models.DTO;
using ThuVien_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

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
		[HttpGet]
		[Route("get-book-by-id/{id}")]
		public IActionResult GetBookById([FromRoute] int id)
		{
			//get book domain model from db
			var bookWithDomain = _dbContext.Book.Where(n => n.BookID == id);
			if (bookWithDomain == null)
			{
				return NotFound();
			}
			//map domain model to DTO
			var bookWithIdDTO = bookWithDomain.Select(Books => new BookDTO()
			{
				Id = Books.BookID,
				Title = Books.Title,
				Description = Books.Description,
				IsRead = Books.IsRead,
				DateRead = Books.IsRead ? Books.DateRead.Value : null,
				Rate = Books.IsRead ? Books.Rate.Value : null,
				Genre = Books.Genre,
				CoverUrl = Books.CoverUrl,
				PublisherName = Books.Publisher.Name,
				AuthorName = Books.Book_Authors.Select(n => n.Author.FullName).ToList()
			});
			return Ok(bookWithIdDTO);
		}
		[HttpPost("add-book")]
		public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
		{
			//   DTO to Domain Model 
			var bookDomainModel = new Books
			{
				Title = addBookRequestDTO.Title,
				Description = addBookRequestDTO.Description,
				IsRead = addBookRequestDTO.IsRead,
				Rate = addBookRequestDTO.Rate,
				Genre =addBookRequestDTO.Genre,
				CoverUrl = addBookRequestDTO.CoverUrl,
				DateAdded = addBookRequestDTO.DateAdded,
				PublisherID = addBookRequestDTO.PublisherID,
			};
			//Use Domain Model to create book
			_dbContext.Book.Add(bookDomainModel);
			_dbContext.SaveChanges();
			foreach (var id in addBookRequestDTO.AuthorIds)
			{
				var _book_author = new Book_Author()
				{
					BookID = bookDomainModel.BookID,
					AuthorID = id
				};
				_dbContext.Book_Author.Add(_book_author);
				_dbContext.SaveChanges();
			}
			return Ok();
		}
		//update book in db
		[HttpPut]
		[Route("update-book/{id}")]
		public IActionResult UpdateBook([FromBody] BookDTO bookDTO, [FromRoute] int id)
		{
			//get book from db
			var bookFromDb = _dbContext.Book.Include(n => n.Book_Authors).FirstOrDefault(n => n.BookID == id);
			if (bookFromDb == null)
			{
				return NotFound();
			}
			//map DTO to domain model
			bookFromDb.Title = bookDTO.Title;
			bookFromDb.Description = bookDTO.Description;
			bookFromDb.IsRead = bookDTO.IsRead;
			bookFromDb.DateRead = bookDTO.DateRead;
			bookFromDb.Rate = bookDTO.Rate;
			bookFromDb.Genre = bookDTO.Genre;
			bookFromDb.CoverUrl = bookDTO.CoverUrl;
			bookFromDb.Publisher.Name = bookDTO.PublisherName;
			bookFromDb.Book_Authors = bookDTO.AuthorName.Select(n => new Book_Author()
			{
				Author = new Authors()
				{
					FullName = n
				}
			}).ToList();
			_dbContext.SaveChanges();
			return Ok();
		}
		//delete book from db
		[HttpDelete]
		[Route("delete-book/{id}")]
		public IActionResult DeleteBook([FromRoute] int id)
		{
			//get book from db
			var bookFromDb = _dbContext.Book.FirstOrDefault(n => n.BookID == id);
			if (bookFromDb == null)
			{
				return NotFound();
			}
			_dbContext.Book.Remove(bookFromDb);
			_dbContext.SaveChanges();
			return Ok();
		}

	}	
	
}

