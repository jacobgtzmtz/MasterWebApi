using Application;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Models;
using Application.Interfaces;
using Infrastructure.Reports;

var builder = WebApplication.CreateBuilder(args);

//Agregar endpoints
builder.Services.AddControllers();

//Agregar base de datos
/* builder.Services.AddDbContext<MasterNetDBContext>(options => 
options.UseNpgsql(builder.Configuration.GetConnectionString("MasterNetDBConnection"))); */
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddTransient(typeof(IReportService<>), typeof(ReportService<>));


//Agregar Identity Service
builder.Services.AddIdentityCore<AppUser>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MasterNetDBContext>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

//Mapeamos los controllers
app.MapControllers();
app.Run();
