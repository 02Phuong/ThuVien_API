using System.ComponentModel.DataAnnotations;

namespace ThuVien_API.Models
{
	public class Publishers
	{
		[Key]
		public int PublisherID { get; set; }	
		public string? Name { get; set; }
		public List<Books> Book { get; set; }
	}
}
