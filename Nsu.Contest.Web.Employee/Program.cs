using Nsu.Contest.Web.Employee.Services;
using Nsu.Contest.Web.Employee.Clients;

using Refit;
using Polly;
using Polly.Extensions.Http;

using MassTransit;
using Nsu.Contest.Web.Common.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmployeeConfig>(
    builder.Configuration.GetSection("EmployeeServiceOptions"));
builder.Services.Configure<ClientConfig>(
    builder.Configuration.GetSection("ClientOptions"));
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddRefitClient<IHRManagerClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"))
    .AddPolicyHandler(GetRetryPolicy(builder));

builder.Services.AddLogging();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContestStartConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});


var app = builder.Build();

app.MapPost("/publish-wishlist", async (IPublishEndpoint publishEndpoint, WishilistMessage message) =>
{
    await publishEndpoint.Publish(message);
    return Results.Ok();
});

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
