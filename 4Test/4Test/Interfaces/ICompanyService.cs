using System;
using Contracts.RequestModels;
using Models;


namespace Interfaces
{


	public interface ICompanyService
	{
		public Task<ValidationError> ValidateCompany(CompanyRequest employee);
		Task<int> CreateCompanyAsync(CompanyRequest employee, ILogService logServicea);
	}

}
