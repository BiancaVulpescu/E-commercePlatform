using Infrastructure;
using Application;
using Microsoft.EntityFrameworkCore.Query;

var builder = WebApplication.CreateBuilder(args);
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection") + dbPassword;
builder.Configuration["ConnectionStrings:DefaultConnection"] = defaultConnection;
// Add services to the container.

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

await app.RunAsync();
public partial class Program
{ 
    protected Program() { }
}
