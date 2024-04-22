using Microsoft.AspNetCore.Mvc;

namespace ThuVien_API.Controllers
{
	public class AuthorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
