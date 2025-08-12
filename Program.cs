using DotNetCoreApiTemplate.Data;
using DotNetCoreApiTemplate.Middleware;
using DotNetCoreApiTemplate.Repositories;
using DotNetCoreApiTemplate.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();


// Add DbContext with SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDbConnection")));

// Bind API Key from appsettings
builder.Services.Configure<ApiKeySettings>(builder.Configuration.GetSection("ApiKeySettings"));

//Swagger with API Key header
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Secure API", Version = "v1" });

c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
{
    In = ParameterLocation.Header,
    Name = "X-API-KEY",
    Type = SecuritySchemeType.ApiKey,
    Description = "Enter the API Key to access this API"
});

c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference
                { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Middleware
//app.UseErrorHandling();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        c.RoutePrefix = string.Empty; // Sets Swagger UI at root (/)
    });
}

// Use API Key middleware
app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();
