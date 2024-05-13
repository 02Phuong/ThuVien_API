using ThuVien_API.Models.DTO;
using ThuVien_API.Models;
using ThuVien_API.Data;

namespace ThuVien_API.Repositories
{
	public class AuthorRepository:IAuthorRepository
	{
		private readonly AppDbContext _dbContext;
		public AuthorRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public List<AuthorDTO> GellAllAuthors()
		{
			//Get Data From Database -Domain Model 
			var allAuthorsDomain = _dbContext.Author.ToList();
			//Map domain models to DTOs 
			var allAuthorDTO = new List<AuthorDTO>();
			foreach (var authorDomain in allAuthorsDomain)
			{
				allAuthorDTO.Add(new AuthorDTO()
				{
					Id = authorDomain.AuthorID,
					FullName = authorDomain.FullName
				});
			}
			//return DTOs 
			return allAuthorDTO;
		}
		public AuthorNoIdDTO GetAuthorById(int id)
		{
			// get book Domain model from Db
			var authorWithIdDomain = _dbContext.Author.FirstOrDefault(x => x.AuthorID ==id);
			if (authorWithIdDomain == null)
			{
				return null;
			}
			//Map Domain Model to DTOs 
			var authorNoIdDTO = new AuthorNoIdDTO
			{
				FullName = authorWithIdDomain.FullName,
			};
			return authorNoIdDTO;
		}
		public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
		{
			var authorDomainModel = new Authors
			{
				FullName = addAuthorRequestDTO.FullName,
			};
			//Use Domain Model to create Author 
			_dbContext.Author.Add(authorDomainModel);
			_dbContext.SaveChanges();
			return addAuthorRequestDTO;
		}
		public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
		{
			var authorDomain = _dbContext.Author.FirstOrDefault(n => n.AuthorID == id);
			if (authorDomain != null)
			{
				authorDomain.FullName = authorNoIdDTO.FullName;
				_dbContext.SaveChanges();
			}
			return authorNoIdDTO;
		}
		public Authors? DeleteAuthorById(int id)
		{
			var authorDomain = _dbContext.Author.FirstOrDefault(n => n.AuthorID == id);
			if (authorDomain != null)
			{
				_dbContext.Author.Remove(authorDomain);
				_dbContext.SaveChanges();
			}
			return null;
		}
		
	}
}

