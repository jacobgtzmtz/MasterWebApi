using Application;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Models;
using Application.Interfaces;
using Infrastructure.Reports;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Agregar endpoints
builder.Services.AddControllers();

//Agregar base de datos
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();

// builder.Services.AddTransient(typeof(IReportService<>), typeof(ReportService<>));
builder.Services.AddScoped(typeof(IReportService<>), typeof(ReportService<>));


//Agregar Identity Service
builder.Services.AddIdentityCore<AppUser>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MasterNetDBContext>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

//Mapeamos los controllers
app.MapControllers();
app.Run();
