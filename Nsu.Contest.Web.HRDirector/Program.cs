using Microsoft.EntityFrameworkCore;
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

builder.Services.AddScoped<IHRDirectorService, HRDirectorService>();
builder.Services.AddScoped<ITeamEstimatingStrategy, HarmonicMean>();
builder.Services.AddScoped<Director>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
