using ThuVien_API.Models.DTO;
using ThuVien_API.Models;


namespace ThuVien_API.Repositories
{
	public interface IBookRepository
	{

		List<BookWithAuthorAndPublisherDTO> GetAllBooks(string? filterOn = null, string?
		filterQuery = null, string? sortBy = null,
	    bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
		BookWithAuthorAndPublisherDTO GetBookById(int id);
		AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
		AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
		Books? DeleteBookById(int id);
		
		
	}
}


