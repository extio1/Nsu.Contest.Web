using Nsu.Contest.Web.Employee.Services;
using Nsu.Contest.Web.Employee.Clients;

using Refit;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmployeeConfig>(
    builder.Configuration.GetSection("EmployeeServiceOptions"));
builder.Services.Configure<ClientConfig>(
    builder.Configuration.GetSection("ClientOptions"));
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddRefitClient<IHRManagerClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"))
    .AddPolicyHandler(GetRetryPolicy(builder));

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddLogging();

var app = builder.Build();

var serviceProvider = app.Services;
var employeeService = serviceProvider.GetRequiredService<EmployeeService>();

await employeeService.SendPreferencesAsync();

app.Run();


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IHostApplicationBuilder builder)
{
    var configSection = builder.Configuration.GetSection("ClientOptions");
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(
            configSection.GetValue<int>("RetryCount"), 
            r => TimeSpan.FromSeconds(r * configSection.GetValue<int>("Interval")));
}
