using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuVien_API.Models
{
	public class Book_Author
	{
		[Key]
		public int ID { get; set; }
		
		public int BookID { get; set; }
		public Books Book { get; set; }

		public int AuthorID { get; set; }
		public Authors Author { get; set; }

	}
}
