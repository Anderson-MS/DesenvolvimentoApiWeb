using Microsoft.EntityFrameworkCore;
using TesteAdecco.Data;
using TesteAdecco.Repositorios;
using TesteAdecco.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<SistemaClienteDBContex>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
    );

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

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
