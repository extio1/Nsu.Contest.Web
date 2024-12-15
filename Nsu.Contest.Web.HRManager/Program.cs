using Microsoft.EntityFrameworkCore;
using Nsu.Contest.Web.HRManager.Model.Config;
using Nsu.Contest.Web.HRManager.Model.Data;
using Nsu.Contest.Web.HRManager.Model.Teambuilding;
using Nsu.Contest.Web.HRManager.Model.Teambuilding.Strategy;
using Nsu.Contest.Web.HRManager.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddDbContext<HRManagerDbContext>(
    options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.Configure<HRManagerConfig>(
    builder.Configuration.GetSection("HRManagerConfig"));

builder.Services.AddScoped<ITeamBuildingStrategy, RandomTeamBuildingStrategy>();
builder.Services.AddScoped<HRManagerService>();
builder.Services.AddScoped<Manager>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
