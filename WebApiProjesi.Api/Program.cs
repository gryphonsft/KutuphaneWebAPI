using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

// Identity ayarları
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is missing")))
    };
});

builder.Services.AddAuthorization();

// UnitOfWork DI kaydı
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository için DI kaydı
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IBookCopyRepository, BookCopyRepository>();
builder.Services.AddScoped<IBorrowRepository, BorrowRepository>();

// Service için DI kaydı
builder.Services.AddScoped<IBorrowService, BorrowService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookCopyService, BookCopyService>();

// Controllers ve partial update
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


app.UseAuthentication();  
app.UseAuthorization();  

app.MapControllers();

app.Run();