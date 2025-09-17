using Microsoft.EntityFrameworkCore;
using SerieHubAPI.Data;
using SerieHubAPI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<UsuarioData>();
builder.Services.AddScoped<SerieData>();
builder.Services.AddScoped<SerieService>();
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(connectionString));
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


