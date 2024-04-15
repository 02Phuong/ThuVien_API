using Microsoft.AspNetCore.Mvc;

namespace ThuVien_API.Controllers
{
	public class BookController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
