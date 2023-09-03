using System;
using Contracts.RequestModels;
using Models;


namespace Interfaces
{


	public interface IEmployeeService
	{
		public Task<ValidationError> ValidateEmployee(EmployeeRequest employee);
		public Task<int> CreateEmployeeAsync(EmployeeRequest employee, ILogService logServicea);
		public Boolean EmployeeExists(int EmployeId);
	}

}
