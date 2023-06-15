using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Middlewares
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;

		/// <summary>
		///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
		/// </summary>
		public LoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		/// <summary>
		///  Необходимо реализовать метод Invoke  или InvokeAsync
		/// </summary>
		public async Task InvokeAsync(HttpContext context, BlogContext dbContext)
		{
			Console.WriteLine($"[{DateTime.Now}]: New request to http//{context.Request.Host.Value + context.Request.Path}");

			Request request = new()
			{
				Id = Guid.NewGuid(),
				Date = DateTime.UtcNow,
				Url = context.Request.Path.ToString()
			};
			dbContext.Requests.Add(request);
			await dbContext.SaveChangesAsync();

			// Передача запроса далее по конвейеру
			await _next.Invoke(context);
		}
	}
}