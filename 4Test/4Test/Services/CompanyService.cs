using DB;
using System;
using Microsoft.EntityFrameworkCore;
using Interfaces;
using Contracts.RequestModels;
using Newtonsoft.Json;
using Models;
using System.ComponentModel.Design;

namespace Services
{
	public class CompanyService : ICompanyService
	{
		private readonly DbContext _dbContext;

		public CompanyService()
		{
			_dbContext = new _4createContext ();

		}

		public async Task<ValidationError> ValidateCompany(CompanyRequest company)
		{ 
			var validationStatus = new ValidationError();
			var dbContext = new _4createContext();

			var exists = await dbContext.Companies.AnyAsync(e => e.Name.ToUpper() == company.Name.ToUpper());
			if (exists)
			{
				validationStatus.Errors.Add ("Company name:"+ company.Name + " is in use...");
				// return if we wish to catch only first error
			}
			return (validationStatus);
		}

		public async Task<int> CreateCompanyAsync(CompanyRequest company, ILogService logServicea)
		{
			DateTime currentTime;
			int companyId;
			try 
			{

				currentTime = DateTime.Now; // if we need date time from different source ( service, DB... ) we can implement here...
				var dbCompany = new Company
				{
					Name = company.Name,
					CreatedAt = currentTime
				};

				_dbContext.Add(dbCompany);

				await _dbContext.SaveChangesAsync();
				companyId = dbCompany.Id;

				// we can log changes in separate DB transaction ( less consistency, better performances and more flexibility - log can be service, file or DB ). 
				string jsonAtributes = JsonConvert.SerializeObject(company);

				var logData = new Systemlog()
				{
					ResourceType= "Company",
					ResourceIdentifier = dbCompany.Id,
					CreatedAt = currentTime,
					Event="create",
					Attributes = jsonAtributes,
					Comment = "new company "+ company.Name + " was created"
				};
				logServicea.Log(logData);

				return companyId;
			}
			catch (Exception e)
			{ 
				// TODO log error...
				return -1;
			}
		}

	}

}
