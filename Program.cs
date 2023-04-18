// Global directives
global using Microsoft.AspNetCore.Mvc;
global using backend.Models;
global using Microsoft.EntityFrameworkCore;
global using backend.Database;

// Local directives
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// For CORS
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => build.WithOrigins("http://127.0.0.1:5173").AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddControllers();

// For auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

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

// For CORS
app.UseCors("corspolicy");

app.UseHttpsRedirection();

// For auth
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
