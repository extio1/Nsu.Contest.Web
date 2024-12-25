using Microsoft.EntityFrameworkCore;
using MassTransit;

using Nsu.Contest.Web.HRDirector.Model.Data;
using Nsu.Contest.Web.HRDirector.Model;
using Nsu.Contest.Web.HRDirector.Model.Strategy;
using Nsu.Contest.Web.HRDirector.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddDbContext<HRDirectorDbContext>(
    options =>  { 
        options.EnableSensitiveDataLogging();
        options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    }
);

builder.Services.Configure<HRDirectorConfig>(
    builder.Configuration.GetSection("HRDirectorConfig"));

builder.Services.AddHostedService<HRDirectorProducer>();
builder.Services.AddHostedService<HRDirectorBackground>();
builder.Services.AddScoped<IHRDirectorService, HRDirectorService>();
builder.Services.AddScoped<ITeamEstimatingStrategy, HarmonicMean>();
builder.Services.AddScoped<Director>();
builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HRDirectorWishlistConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("director-wishlist-queue", e =>
        {
            e.ConfigureConsumer<HRDirectorWishlistConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
