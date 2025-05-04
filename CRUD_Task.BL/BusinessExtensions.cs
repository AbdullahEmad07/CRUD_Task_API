using CRUD_Task.BL.Managers.Employee;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRUD_Task.BL
{
	public static class BusinessExtensions
	{
		public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IEmployeeManager, EmployeeManager>();
		}
	}
}
