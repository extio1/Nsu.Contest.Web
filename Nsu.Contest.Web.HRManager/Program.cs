using Microsoft.EntityFrameworkCore;
using Nsu.Contest.Web.HRManager.Clients;
using Nsu.Contest.Web.HRManager.Model.Config;
using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;
using Nsu.Contest.Web.HRManager.Model.Teambuilding.Strategy;
using Nsu.Contest.Web.HRManager.Services;

using Refit;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddDbContext<HRManagerDbContext>(
    options =>  { options.EnableSensitiveDataLogging();
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));});

builder.Services.AddRefitClient<IHRDirectorClient>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://hrmanager"));
builder.Services.Configure<HRManagerConfig>(
    builder.Configuration.GetSection("HRManagerConfig"));

builder.Services.AddScoped<ITeamBuildingStrategy, RandomTeamBuildingStrategy>();
builder.Services.AddScoped<IHRManagerService, HRManagerService>();
builder.Services.AddScoped<Manager>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
