using Microsoft.AspNetCore.Mvc;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

//Agregar endpoints
builder.Services.AddControllers();

//Agregar base de datos
builder.Services.AddDbContext<MasterNetDBContext>();

var app = builder.Build();

//Mapeamos los controllers
app.MapControllers();
app.Run();
