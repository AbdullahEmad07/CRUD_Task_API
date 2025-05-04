using CRUD_Task.BL.DTOs;
using CRUD_Task.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Task.BL.Managers.Employee
{
	public class EmployeeManager : IEmployeeManager
	{
		private readonly UserManager<DAL.Models.Employee> _userManager;

		public EmployeeManager(UserManager<DAL.Models.Employee> userManager)
		{
			_userManager=userManager;
		}
		public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
		{
			var employees=await _userManager.Users.ToListAsync();
			var employeesDto = employees.Select(e => new EmployeeDto
			{
				Id = e.Id,
				FirstName = e.FirstName,
				LastName = e.LastName,
				Email = e.Email,
				Position = e.Position
			}).ToList();
			return employeesDto;
		}
		///////////////////////////////////////////////////////////////////
		public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
		{
			var employee=await _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
			if (employee == null)
			{
				return null;
			}
			var employeeDto = new EmployeeDto
			{
				Id = employee.Id,
				FirstName = employee.FirstName,
				LastName = employee.LastName,
				Email = employee.Email,
				Position = employee.Position
			};
			return employeeDto;
		}
		///////////////////////////////////////////////////////////////////
		public async Task<bool> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
		{
			var employee = new DAL.Models.Employee
			{
				UserName=addEmployeeDto.FirstName,
				FirstName = addEmployeeDto.FirstName,
				LastName = addEmployeeDto.LastName,
				Email = addEmployeeDto.Email,
				Position = addEmployeeDto.Position
			};
			var creationResult= await _userManager.CreateAsync(employee);
			if(!creationResult.Succeeded)
			{
				return false;
			}
			return true;
		}

		///////////////////////////////////////////////////////////////////
		public async Task<bool> UpdateEmployeeAsync(int id, AddEmployeeDto addEmployeeDto)
		{
			var employee = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
			if (employee == null)
			{
				return false;
			}
			employee.UserName = addEmployeeDto.FirstName;
			employee.FirstName = addEmployeeDto.FirstName;
			employee.LastName = addEmployeeDto.LastName;
			employee.Email = addEmployeeDto.Email;
			employee.Position = addEmployeeDto.Position;
			var updateResult= await _userManager.UpdateAsync(employee);
			if(!updateResult.Succeeded)
			{
				return false;
			}
			return true;
		}
		///////////////////////////////////////////////////////////////////
		public async Task<bool> DeleteEmployeeAsync(int id)
		{
			var employee = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
			if (employee == null)
			{
				return false;
			}
			var deleteResult = await _userManager.DeleteAsync(employee);
			if(!deleteResult.Succeeded)
			{
				return false;
			}
			return true;
		}

		
	}
}
