using ThuVien_API.Data;
using ThuVien_API.Models.DTO;
using ThuVien_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ThuVien_API.Repositories
{
	public class BookRepository: IBookRepository
	{
		private readonly AppDbContext _dbContext;
		public BookRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public List<BookWithAuthorAndPublisherDTO> GetAllBooks(string? filterOn = null, string? filterQuery = null,
			string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
		{
			var allBooks = _dbContext.Book.Select(Books => new BookWithAuthorAndPublisherDTO()
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
			}).AsQueryable();
			//filtering 
			if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
			{
				if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
				{
					allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
				}
			}

			//sorting 
			if (string.IsNullOrWhiteSpace(sortBy) == false)
			{
				if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
				{
					allBooks = isAscending ? allBooks.OrderBy(x => x.Title) :allBooks.OrderByDescending(x => x.Title);
				}
			}
			//pagination 
			var skipResults = (pageNumber - 1) * pageSize;
			return allBooks.Skip(skipResults).Take(pageSize).ToList();

		}
		public BookWithAuthorAndPublisherDTO GetBookById(int id)
		{
			var bookWithDomain = _dbContext.Book.Where(n => n.BookID == id);
			//Map Domain Model to DTOs
			var bookWithIdDTO = bookWithDomain.Select(book => new BookWithAuthorAndPublisherDTO()
			{
				Id = book.BookID,
				Title = book.Title,
				Description = book.Description,
				IsRead = book.IsRead,
				DateRead = book.DateRead,
				Rate = book.Rate,
				Genre = book.Genre,
				CoverUrl = book.CoverUrl,
				PublisherName = book.Publisher.Name,
				AuthorName = book.Book_Authors.Select(n => n.Author.FullName).ToList()
			}).FirstOrDefault();
			return bookWithIdDTO;
		}
		public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
		{
			//map DTO to Domain Model
			var bookDomainModel = new Books
			{
				Title = addBookRequestDTO.Title,
				Description = addBookRequestDTO.Description,
				IsRead = addBookRequestDTO.IsRead,
				DateRead = addBookRequestDTO.DateRead,
				Rate = addBookRequestDTO.Rate,
				Genre = addBookRequestDTO.Genre,
				CoverUrl = addBookRequestDTO.CoverUrl,
				DateAdded = addBookRequestDTO.DateAdded,
				PublisherID = addBookRequestDTO.PublisherID
			};
			//Use Domain Model to add Book 
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
			return addBookRequestDTO;
		}
		public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO)
		{
			var bookDomain = _dbContext.Book.FirstOrDefault(n => n.BookID == id);
			if (bookDomain != null)
			{
				bookDomain.Title = bookDTO.Title;
				bookDomain.Description = bookDTO.Description;
				bookDomain.IsRead = bookDTO.IsRead;
				bookDomain.DateRead = bookDTO.DateRead;
				bookDomain.Rate = bookDTO.Rate;
				bookDomain.Genre = bookDTO.Genre;
				bookDomain.CoverUrl = bookDTO.CoverUrl;
				bookDomain.DateAdded = bookDTO.DateAdded;
				bookDomain.PublisherID = bookDTO.PublisherID;
				_dbContext.SaveChanges();
			}
			var authorDomain = _dbContext.Book_Author.Where(a => a.BookID == id).ToList();
			if (authorDomain != null)
			{
				_dbContext.Book_Author.RemoveRange(authorDomain);
				_dbContext.SaveChanges();
			}
			foreach (var authorid in bookDTO.AuthorIds)
			{
				var _book_author = new Book_Author()
				{
					BookID = id,
					AuthorID = authorid
				};
				_dbContext.Book_Author.Add(_book_author);
				_dbContext.SaveChanges();
			}
			return bookDTO;
		}
		public Books? DeleteBookById(int id)
		{
			var bookDomain = _dbContext.Book.FirstOrDefault(n => n. BookID== id);
			if (bookDomain != null)
			{
				_dbContext.Book.Remove(bookDomain);
				_dbContext.SaveChanges();
			}
			return bookDomain;
		}
	}
} 

	