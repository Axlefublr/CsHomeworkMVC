using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Controllers
{
	public class UsersController : Controller
	{
		private readonly IBlogRepository _repo;
		public UsersController(IBlogRepository repo)
		{
			_repo = repo;
		}
		public async Task<IActionResult> Index()
		{
			var authors = await _repo.GetUsers();
			return View(authors);
		}

		public async Task<IActionResult> Register()
		{
			return View();
		}
	}
}