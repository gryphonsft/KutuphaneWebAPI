using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Application.Services;
using WebApiProjesi.Domain.Interfaces;
using WebApiProjesi.Domain.Role;
using WebApiProjesi.Domain.User;
using WebApiProjesi.Infrastructure.Data;
using WebApiProjesi.Infrastructure.Persistence;
using WebApiProjesi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        x => x.MigrationsAssembly("WebApiProjesi.Infrastructure")));

// Identity ayarlar�
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//UnitOfWork DI kayd�
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository i�in DI kayd�
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IBookCopyRepository,BookCopyRepository >();
builder.Services.AddScoped<IBorrowRepository,BorrowRepository >();

// Service i�in DI kayd�
builder.Services.AddScoped<IBorrowService, BorrowService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookCopyService, BookCopyService>();

// Controllers ve tabiki partial update
builder.Services.AddControllers()
    .AddNewtonsoftJson(); 

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiProjesi API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
