using DB;
using System;
using System.Collections.Generic;

namespace Contracts.RequestModels
{

	public class EmployeeInCompany
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Email { get; set; }
	}

	public class CompanyRequest
    {
        public string Name { get; set; }
		public List<EmployeeInCompany> Employees { get; set; }
		public CompanyRequest()
		{
			Employees = new List<EmployeeInCompany>();
		}
	}
}
