using Microsoft.AspNetCore.Mvc;
using Contracts.RequestModels;
using Interfaces;
using System.Runtime.CompilerServices;

namespace _4Test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EmployeeController : ControllerBase
	{	
		private IEmployeeService _employeeServicea;
		private ILogService _logServicea;

		private static readonly object _lock = new object();

		public EmployeeController(IEmployeeService employeeServicea, ILogService logServicea)
		{
			_employeeServicea = employeeServicea;
			_logServicea = logServicea;
		}


		[HttpPost]
		[Route("create")]
		public async Task<ActionResult<StandardResponse>> CreateAsync(EmployeeRequest employee)
		{
			var response = new StandardResponse();

			// maybe lock to be sure nothing changes between validation and insert. 
			// Howeever, consistency can be broken from other programs or DB. Unique constraint in DB can resolve this problem...
			// it can have bad impact to performances


			// TODO Add simple validation ( mail.... )


			var validationStatus = await _employeeServicea.ValidateEmployee(employee);
			if (validationStatus.Errors.Count() > 0)
			{ 
				// only first error will be returned
				// TODO - return all errors
				response.errorCode = -100; // TODO add some enumeration for errors
				response.errorText = validationStatus.Errors[0];
				return (response);
			}

			int serviceSuccess = await _employeeServicea.CreateEmployeeAsync(employee, _logServicea);
			if (serviceSuccess == 0)
				return Ok(response);
			else
				return StatusCode(500, "System error");


		}
	}
}