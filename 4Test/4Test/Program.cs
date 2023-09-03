using DB;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();


// TODO Encrypt / decrypt connection string and read from config...


// if log is in DB we can used Scoped. For file loger iti is better to use Singleton
builder.Services.AddScoped<ILogService, LogService>();


// TODO Set CORS rules
/*builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
		   builder =>
		   {
			   builder.WithOrigins("http://localhost")
							 .AllowAnyMethod()
							 .AllowAnyHeader()
							 .AllowCredentials();


		   }));
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
