using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Agregar endpoints
builder.Services.AddControllers();

var app = builder.Build();

//Mapeamos los controllers
app.MapControllers();
app.Run();
