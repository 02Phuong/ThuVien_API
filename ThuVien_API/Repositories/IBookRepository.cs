using ThuVien_API.Models.DTO;
using ThuVien_API.Models;


namespace ThuVien_API.Repositories
{
	public interface IBookRepository
	{
		
			List<BookWithAuthorAndPublisherDTO> GetAllBooks();
			BookWithAuthorAndPublisherDTO GetBookById(int id);
			AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
			AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
			Books? DeleteBookById(int id);
	}
}


