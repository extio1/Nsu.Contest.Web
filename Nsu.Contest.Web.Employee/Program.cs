using Nsu.Contest.Web.Common.Entity;
using Nsu.Contest.Web.Employee.Services;
using Nsu.Contest.Web.Employee.Clients;

using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitClient<IHRManagerClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"));
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddLogging();

var app = builder.Build();

var serviceProvider = app.Services;
var employeeService = serviceProvider.GetRequiredService<EmployeeService>();

await employeeService.SendPreferencesAsync();

app.Run();
