using Infrastructure;
using Application;
using Identity;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "MyAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:4200");
                      policy.AllowAnyHeader();
                      policy.AllowAnyMethod();
                    });
});
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection") + dbPassword;
builder.Configuration["ConnectionStrings:DefaultConnection"] = defaultConnection;
// Add services to the container.

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
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
app.UseStaticFiles();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
public partial class Program
{ 
    protected Program() { }
}
