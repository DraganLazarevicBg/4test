using Microsoft.AspNetCore.Mvc;
using Contracts.RequestModels;
using Interfaces;
using System.Runtime.CompilerServices;
using DB;

namespace _4Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CompanyController : ControllerBase
	{	
		private ICompanyService _companyServicea;
		private IEmployeeService _employeeServicea;
		private ILogService _logServicea;

		private static readonly object _lock = new object();

		public CompanyController(ICompanyService companyServicea, ILogService logServicea, IEmployeeService employeeServicea)
		{
			_companyServicea = companyServicea;
			_logServicea = logServicea;
			_employeeServicea = employeeServicea;
		}


		[HttpPost]
		[Route("create")]
		public async Task<ActionResult<StandardResponse>> CreateAsync(CompanyRequest company)
		{
			EmployeeRequest employee;
			int serviceSuccess, ret, companiId;
			var response = new StandardResponse();

			// maybe lock to be sure nothing changes between validation and insert. 
			// Howeever, consistency can be broken from other programs or DB. Unique constraint in DB can resolve this problem...
			// it can have bad impact to performances


			// TODO Add simple validation ( mail.... )




			var validationStatus = await _companyServicea.ValidateCompany(company);
			if (validationStatus.Errors.Count() > 0)
			{ 
				// only first error will be returned
				// TODO - return all errors
				response.errorCode = -100; // TODO add some enumeration for errors
				response.errorText = validationStatus.Errors[0];
				return (response);
			}
			// validate employees
			foreach (var e in company.Employees)
			{
				if (!_employeeServicea.EmployeeExists(e.Id))
				{
					employee = new EmployeeRequest()
					{
						Title = e.Title,
						Email = e.Email
					};
					validationStatus = await _employeeServicea.ValidateEmployee(employee);
					if (validationStatus.Errors.Count() > 0)
					{
						// only first error will be returned
						// TODO - return all errors
						response.errorCode = -100; // TODO add some enumeration for errors
						response.errorText = validationStatus.Errors[0];
						return (response);
					}
				}
			}

			// create company
			ret = await _companyServicea.CreateCompanyAsync(company, _logServicea);
			if (ret < 0)
				return StatusCode(500, "System error");
			else
				companiId = ret;

			// create employees
			foreach (var e in company.Employees)
			{
				if (!_employeeServicea.EmployeeExists(e.Id))
				{
					employee = new EmployeeRequest()
					{
						Title = e.Title,
						Email = e.Email
					};
					employee.Companies.Add (companiId);
					serviceSuccess = await _employeeServicea.CreateEmployeeAsync(employee, _logServicea);
					if (serviceSuccess != 0)
						return StatusCode(500, "System error");
				}
			}
			return Ok(response);

		}
	}
}