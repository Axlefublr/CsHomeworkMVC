using System.Threading.Tasks;

namespace MvcStartApp
{
	public interface IBlogRepository
	{
		Task AddUser(User user);
		Task<User[]> GetUsers();
	}
}