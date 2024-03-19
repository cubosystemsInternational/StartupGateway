using Microsoft.EntityFrameworkCore;
using StartupGateway.BusinessEntities;
using StartupGateway.BusinessLogic;
using StartupGateway.DAL;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW;
using StartupGateway.UoW.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(builder =>
{
    // Configure logging providers as needed (e.g., Console, Debug, File)
    builder.AddConsole();
    builder.AddDebug();
    // Add other logging providers if required
});
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProjectDAL<Project>, ProjectDAL<Project>>();
builder.Services.AddScoped<ProjectBLL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
