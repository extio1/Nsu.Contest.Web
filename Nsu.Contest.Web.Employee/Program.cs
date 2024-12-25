using Nsu.Contest.Web.Employee.Services;
using Nsu.Contest.Web.Employee.Clients;

using Refit;
using Polly;
using Polly.Extensions.Http;

using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmployeeConfig>(
    builder.Configuration.GetSection("EmployeeServiceOptions"));
builder.Services.Configure<ClientConfig>(
    builder.Configuration.GetSection("ClientOptions"));
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddRefitClient<IHRManagerClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"))
    .AddPolicyHandler(GetRetryPolicy(builder));

builder.Services.AddHostedService<EmployeeConsumer>();
builder.Services.AddLogging();

var app = builder.Build();
app.Run();

var employeeType = Environment.GetEnvironmentVariable("EMPLOYEEOPTIONS__EMPLOYEETYPE") ?? "Unknown";
var employeeId = Environment.GetEnvironmentVariable("EMPLOYEEOPTIONS__EMPLOYEEID") ?? "Unknown";

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContestStartConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://rabbitmq", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint($"hackathon-started-queue-{employeeType}-{employeeId}", e =>
        {
            e.ConfigureConsumer<ContestStartConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});


static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IHostApplicationBuilder builder)
{
    var configSection = builder.Configuration.GetSection("ClientOptions");
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(
            configSection.GetValue<int>("RetryCount"), 
            r => TimeSpan.FromSeconds(r * configSection.GetValue<int>("Interval")));
}
