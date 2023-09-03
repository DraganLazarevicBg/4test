using DB;
using System;
using System.Collections.Generic;

namespace Contracts.RequestModels
{
    public class EmployeeRequest
    {
        public string Title { get; set; }
        public string Email { get; set; }
		public List<int> Companies { get; set; }

        public EmployeeRequest()
        {
			Companies = new List<int>();
		}
	}
}
