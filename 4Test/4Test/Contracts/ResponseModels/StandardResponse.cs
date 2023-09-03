using DB;
using System;
using System.Collections.Generic;

namespace Contracts.RequestModels
{
    public class StandardResponse
    {
        public int errorCode { get; set; }
        public string errorText { get; set; }

		public StandardResponse()
        {
			errorCode=0;
			errorText="";
		}
	}
}
