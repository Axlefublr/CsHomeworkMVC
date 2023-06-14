using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcStartApp.Models;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBlogRepository _repo;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
		{
			_logger = logger;
			_repo = repo;
		}

		public async Task<IActionResult> Index()
		{
			User newUser = new()
			{
				Id = Guid.NewGuid(),
				FirstName = "Andrey",
				LastName = "Petrov",
				JoinDate = DateTime.Now
			};
			await _repo.AddUser(newUser);
			Console.WriteLine($"User with id {newUser.Id}, named {newUser.FirstName} was successfully added on {newUser.JoinDate}");
			return View();
		}

		public async Task<IActionResult> Authors()
		{
			var authors = await _repo.GetUsers();
			return View(authors);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
