using Microsoft.AspNetCore.Mvc;
using ThuVien_API.Data;
using ThuVien_API.Models.DTO;
using ThuVien_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ThuVien_API.Repositories;

namespace ThuVien_API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : Controller
	{
		private readonly AppDbContext _dbContext;
		private readonly IBookRepository _bookRepository;
		public BookController(AppDbContext dbContext, IBookRepository bookRepository)
		{
			_dbContext = dbContext;
			_bookRepository = bookRepository;
		}
		//GET http://localhost:port/api/get-all-books
		[HttpGet("get-all-books")]
		public IActionResult GetAll()
		{
			// su dung reposity pattern 
			var allBooks = _bookRepository.GetAllBooks();
			return Ok(allBooks);
		}


		// GET: api/Students/5
		[HttpGet]
		[Route("get-book-by-id/{id}")]
		public IActionResult GetBookById([FromRoute] int id)
		{
			var bookWithIdDTO = _bookRepository.GetBookById(id);
			return Ok(bookWithIdDTO);
		}
		[HttpPost("add-book")]
		public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
		{
			if (ModelState.IsValid)
			{
				var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
				return Ok(bookAdd);
			}
			else return BadRequest(ModelState);
		}
		[HttpPut("update-book-by-id/{id}")]
		public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
		{
			var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
			return Ok(updateBook);
		}
		[HttpDelete("delete-book-by-id/{id}")]
		public IActionResult DeleteBookById(int id)
		{
			var deleteBook = _bookRepository.DeleteBookById(id);
			return Ok(deleteBook);
		}


	}

}

