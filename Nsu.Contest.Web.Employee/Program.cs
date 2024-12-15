using Nsu.Contest.Web.Employee.Services;
using Nsu.Contest.Web.Employee.Clients;

using Refit;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmployeeConfig>(
    builder.Configuration.GetSection("EmployeeServiceOptions"));

// Добавляем переменные окружения
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddRefitClient<IHRManagerClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"));
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddLogging();

var app = builder.Build();

var serviceProvider = app.Services;
var employeeService = serviceProvider.GetRequiredService<EmployeeService>();

await employeeService.SendPreferencesAsync();

app.Run();
