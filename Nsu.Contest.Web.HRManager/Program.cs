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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HRManagerDbContext>();
    try
    {
        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            Console.WriteLine("Applying pending database migrations...");
            dbContext.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully.");
        }
        else
        {
            Console.WriteLine("No pending migrations. Database is up-to-date.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while checking or applying migrations: {ex.Message}");
    }
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
