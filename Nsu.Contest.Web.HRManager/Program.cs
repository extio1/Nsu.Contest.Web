using Microsoft.EntityFrameworkCore;
using Nsu.Contest.Web.HRManager.Clients;
using Nsu.Contest.Web.HRManager.Model.Config;
using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;
using Nsu.Contest.Web.HRManager.Model.Teambuilding.Strategy;
using Nsu.Contest.Web.HRManager.Services;

using Refit;
using Polly;
using Polly.Extensions.Http;
using HrManager.Service;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddDbContext<HRManagerDbContext>(
    options =>  { options.EnableSensitiveDataLogging();
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));});

builder.Services.AddRefitClient<IHRDirectorClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"))
    .AddPolicyHandler(GetRetryPolicy(builder));

builder.Services.Configure<HRManagerConfig>(
    builder.Configuration.GetSection("HRManagerConfig"));

builder.Services.AddHostedService<HRManagerBackgrond>();
builder.Services.AddScoped<ITeamBuildingStrategy, RandomTeamBuildingStrategy>();
builder.Services.AddScoped<IHRManagerService, HRManagerService>();
builder.Services.AddScoped<Manager>();
builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HRManagerWishlistConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("manager-wishlist-queue", e =>
        {
            e.ConfigureConsumer<HRManagerWishlistConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IHostApplicationBuilder builder)
{
    var configSection = builder.Configuration.GetSection("HRManagerConfig");
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(
            configSection.GetValue<int>("SenderRetryCount"), 
            r => TimeSpan.FromSeconds(r * configSection.GetValue<int>("SenderInterval")));
}
