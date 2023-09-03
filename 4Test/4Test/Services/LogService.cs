using DB;
using System;
using Microsoft.EntityFrameworkCore;
using Interfaces;

namespace Services
{
	public class LogService : ILogService
	{
		private readonly DbContext _dbContext;

		public LogService()
		{
			_dbContext = new DB._4createContext();
		}

		public void Log(Systemlog logData)
		{
			try 
			{
				_dbContext.Add (logData);
				_dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{ 
				// log error...
			}
		}

	}

}
