using Microsoft.AspNetCore.Identity;

namespace CRUD_Task.DAL.Models
{
	public class Employee:IdentityUser<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Position { get; set; }
	}
}
