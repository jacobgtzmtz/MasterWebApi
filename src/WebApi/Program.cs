using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

//Agregar endpoints
builder.Services.AddControllers();

//Agregar base de datos
/* builder.Services.AddDbContext<MasterNetDBContext>(options => 
options.UseNpgsql(builder.Configuration.GetConnectionString("MasterNetDBConnection"))); */
builder.Services.AddPersistence(builder.Configuration);

//Agregar Identity Service
builder.Services.AddIdentityCore<AppUser>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MasterNetDBContext>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

//Mapeamos los controllers
app.MapControllers();
app.Run();
