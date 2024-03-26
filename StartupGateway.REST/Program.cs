using Microsoft.EntityFrameworkCore;
using StartupGateway.DAL;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW;
using StartupGateway.UoW.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")??"");
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserDAL, UserDAL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.Run();

