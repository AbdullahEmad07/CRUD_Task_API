using CRUD_Task.BL.DTOs;
using CRUD_Task.DAL.Models;

namespace CRUD_Task.BL.Managers.Employee
{
	public interface IEmployeeManager
	{
		Task<List<EmployeeDto>> GetAllEmployeesAsync();
		Task<EmployeeDto> GetEmployeeByIdAsync(int id);
		Task<bool> AddEmployeeAsync(AddEmployeeDto addEmployeeDto);
		Task<bool> UpdateEmployeeAsync(int id, AddEmployeeDto addEmployeeDto);
		Task<bool> DeleteEmployeeAsync(int id);
	}
}
