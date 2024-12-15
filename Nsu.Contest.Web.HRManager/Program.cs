using Microsoft.EntityFrameworkCore;
// using Nsu.Contest.Web.HRManager.Data;
using Nsu.Contest.Web.HRManager.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:80");

// builder.Services.AddDbContext<HRManagerDbContext>(options =>
//     options.UseInMemoryDatabase("HRManagerDB"));

builder.Services.AddScoped<HRManagerService>();
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
