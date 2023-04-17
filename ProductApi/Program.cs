using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Repository.Abstract;
using ProductApi.Repository.Concrete;
using ProductApi.Validation;
using ProductApi.ViewModels;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Add Entity framework 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion("8.0.32")));

//Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin"); // Sadece "Admin" rolüne sahip kullanýcýlarý kabul eder
    });
});

//Add Authentication Schema
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            RoleClaimType = "Admin",
            ValidateIssuerSigningKey = true,
            ValidIssuer = "valid_issuer",
            ValidAudience = "valid_audience",
            IssuerSigningKey = new SymmetricSecurityKey(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(builder.Configuration.GetConnectionString("SecretKey"))))
        };
    });

//Add Fluent Validation
builder.Services.AddSingleton<IValidator<Product>, ProductValidator>();
builder.Services.AddSingleton<IValidator<Category>, CategoryValidator>();
builder.Services.AddSingleton<IValidator<Seller>, SellerValidator>();
builder.Services.AddSingleton<IValidator<SellerRegisterViewModel>, SellerRegisterValidator>();

//Adding Repositories
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Seller>, SellerRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();


//AutoMapper
builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

//Add CORS Policy
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("ProductsCorsPolicy", opt =>
    {
        opt.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE", "PATCH");
    });
});

builder.Services.AddEndpointsApiExplorer();

//Swagger arayüzü ayarlamalarý
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ProductWeb Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                {
                                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Use cors policy that has been created before
app.UseCors("ProductsCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
