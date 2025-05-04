using CRUD_Task.BL.DTOs;
using CRUD_Task.BL.Managers.Employee;
using CRUD_Task.DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Task.Controllers.Employees
{
	[ApiController]
	[Route("api/employees")]
	public class EmployeeController:ControllerBase
	{
		private readonly IEmployeeManager _employeeManager;
		private readonly UserManager<Employee> _userManager;

		public EmployeeController(IEmployeeManager employeeManager,
			UserManager<Employee> userManager)
		{
			_employeeManager= employeeManager;
			_userManager= userManager;
		}
		[HttpGet]
		public async Task<Ok<List<EmployeeDto>>> GetAllAsync()
		{
			var employees= await _employeeManager.GetAllEmployeesAsync();
			return TypedResults.Ok(employees);
		}
		/////////////////////////////////////////////////////////////////////////
		[HttpGet]
		[Route("{id}")]
		public async Task<Results<Ok<EmployeeDto>,NotFound>>GetByIdAsync(int id)
		{
			var employee=await _employeeManager.GetEmployeeByIdAsync(id);
			if (employee == null)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.Ok(employee);
		}
		/////////////////////////////////////////////////////////////////////////
		[HttpPost]
		public async Task<Results<Ok<string>,BadRequest<List<string>>>>AddAsync(AddEmployeeDto addEmployeeDto)
		{
			var success=await _employeeManager.AddEmployeeAsync(addEmployeeDto);
			if (!success)
			{
				var employee = new Employee
				{
					UserName = addEmployeeDto.FirstName,
					FirstName = addEmployeeDto.FirstName,
					LastName = addEmployeeDto.LastName,
					Email = addEmployeeDto.Email,
					Position = addEmployeeDto.Position
				};
				var creationResult = await _userManager.CreateAsync(employee);
				var errors=creationResult.Errors.Select(e=>e.Description).ToList();
				return TypedResults.BadRequest(errors);
			}
			return TypedResults.Ok("Employee added successfully");
		}
		/////////////////////////////////////////////////////////////////////////
		[HttpPut]
		[Route("{id}")]
		public async Task<Results<Ok<string>, BadRequest<List<string>>,NotFound>> 
			UpdateAsync(int id, AddEmployeeDto addEmployeeDto)
		{
			var success=await _employeeManager.UpdateEmployeeAsync(id, addEmployeeDto);
			if (!success)
			{
				var employee = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == id);
				if (employee == null)
				{
					return TypedResults.NotFound();
				}
				employee.UserName = addEmployeeDto.FirstName;
				employee.FirstName = addEmployeeDto.FirstName;
				employee.LastName = addEmployeeDto.LastName;
				employee.Email = addEmployeeDto.Email;
				employee.Position = addEmployeeDto.Position;
				var updateResult = await _userManager.UpdateAsync(employee);
				var errors = updateResult.Errors.Select(e => e.Description).ToList();
				return TypedResults.BadRequest(errors);
			}
			return TypedResults.Ok("Employee updated successfully");
		}
		/////////////////////////////////////////////////////////////////////////
		[HttpDelete]
		[Route("{id}")]
		public async Task<Results<Ok<string>, NotFound>> DeleteAsync(int id)
		{
			var success=await _employeeManager.DeleteEmployeeAsync(id);
			if (!success)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.Ok("Employee deleted successfully");
		}
	}
}
