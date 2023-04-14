// Global directives
global using backend.Models;
global using Microsoft.EntityFrameworkCore;
global using backend.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// For CORS
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => build.WithOrigins("http://127.0.0.1:5173").AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection of DbContext class
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
