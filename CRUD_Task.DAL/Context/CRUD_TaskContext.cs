using CRUD_Task.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Task.DAL.Context
{
	public class CRUD_TaskContext:IdentityDbContext<Employee, EmployeeRole,int>
	{
		public DbSet<Employee> Employees => Set<Employee>();
		public CRUD_TaskContext(DbContextOptions<CRUD_TaskContext> options)
			: base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Employee>(e =>
			{
				e.ToTable("Employees");
			});
		}
	}
}
