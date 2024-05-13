using ThuVien_API.Models.DTO;
using ThuVien_API.Models;

namespace ThuVien_API.Repositories
{
	public interface IAuthorRepository
	{
		List<AuthorDTO> GellAllAuthors();
		AuthorNoIdDTO GetAuthorById(int id);
		AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
		AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);
		Authors? DeleteAuthorById(int id);
	}
}
