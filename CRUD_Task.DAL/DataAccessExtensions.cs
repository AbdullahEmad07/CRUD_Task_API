using CRUD_Task.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_Task.DAL
{
	public static class DataAccessExtensions
	{
		public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("Default");
			Console.WriteLine("CONN STRING: " + connectionString);
			services.AddDbContext<CRUD_TaskContext>(options => options.UseSqlServer(connectionString));
		}
	}
}
