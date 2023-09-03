using DB;
using System;
using Microsoft.EntityFrameworkCore;
using Interfaces;
using Contracts.RequestModels;
using Newtonsoft.Json;
using Models;

namespace Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly DbContext _dbContext;

		public EmployeeService()
		{
			_dbContext = new _4createContext ();

		}

		public Boolean EmployeeExists(int EmployeId)
		{	
			var dbContext = new _4createContext();

			var exists = dbContext.Employees.AnyAsync(e => e.Id == EmployeId);

			return (exists.Result);
		}

		public async Task<ValidationError> ValidateEmployee(EmployeeRequest employee)
		{ 
			var validationStatus = new ValidationError();
			var dbContext = new _4createContext();

			if (employee.Companies != null)
			{ 
				var duplicateCompanies = employee.Companies.GroupBy(x => x).Where(group => group.Count() > 1);

				if (duplicateCompanies.Any())
				{
					validationStatus.Errors.Add("Bad requestm duplicate company IDs");
					// return if we wish to catch only first error
				}
			

				var query = from c in dbContext.Companies
							where employee.Companies.Contains(c.Id)
							join w in dbContext.Works on c.Id equals w.Comany
							join e in dbContext.Employees on w.Employee equals e.Id
							where e.Title == employee.Title
							select new
							{
								CompanyMsg = c.Id,
								TitleMsg = e.Title
							};

				foreach (var result in query)
				{
					validationStatus.Errors.Add(result.TitleMsg +" works in "+ result.CompanyMsg);
					// return if we wish to catch only first error
				}
			}

			var exists = await dbContext.Employees.AnyAsync(e => e.Email.ToUpper() == employee.Email.ToUpper());
			if (exists)
			{
				validationStatus.Errors.Add ("Email:"+ employee.Email +" is in use...");
				// return if we wish to catch only first error
			}

			return (validationStatus);
		}

		public async Task<int> CreateEmployeeAsync(EmployeeRequest employee, ILogService logServicea)
		{
			DateTime currentTime;
			try 
			{

				currentTime = DateTime.Now; // if we need date time from different source ( service, DB... ) we can implement here...
				var dbEmploye = new Employee
				{
					Email = employee.Email,
					Title = employee.Title,
					CreatedAt = currentTime
				};

				if (employee.Companies != null)
				{
					var worksList = employee.Companies.Select(intValue => new DB.Work { Comany = intValue }).ToList();

					dbEmploye.Works = worksList;
				}

				_dbContext.Add(dbEmploye);

				await _dbContext.SaveChangesAsync();

				// we can log changes in separate DB transaction ( less consistency, better performances and more flexibility - log can be service, file or DB ). 
				string jsonAtributes = JsonConvert.SerializeObject(employee);

				var logData = new Systemlog()
				{
					ResourceType= "Employee",
					ResourceIdentifier = dbEmploye.Id,
					CreatedAt = currentTime,
					Event="create",
					Attributes = jsonAtributes,
					Comment = "new employee "+ employee.Email + " was created"
				};
				logServicea.Log(logData);

				return 0;
			}
			catch (Exception e)
			{ 
				// TODO log error...
				return -1;
			}
		}

	}

}
