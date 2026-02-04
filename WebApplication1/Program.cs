using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Repositories;
using ChineseAuctionProject.Services;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Auth
var jwtSection = builder.Configuration.GetSection("Jwt");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"] ?? string.Empty));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = signingKey,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

builder.Services.AddAuthorization();

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IOrderManagementRepository, OrderManagementRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Register Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IOrderManagmentService, OrderManagementService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();