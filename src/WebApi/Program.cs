using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;

var builder = WebApplication.CreateBuilder(args);

//Agregar endpoints
builder.Services.AddControllers();

//Agregar base de datos
builder.Services.AddDbContext<MasterNetDBContext>(options => 
options.UseNpgsql(builder.Configuration.GetConnectionString("MasterNetDBConnection")));

//Agregar Identity Service
builder.Services.AddIdentityCore<AppUser>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MasterNetDBContext>();

var app = builder.Build();

//Mapeamos los controllers
app.MapControllers();
app.Run();
