using ASP.NetCoreWebApi.common;
using ASP.NetCoreWebApi.common.IService;
using ASP.NetCoreWebApi.common.Middlewares;
using ASP.NetCoreWebApi.EFCore;
using ASP.NetCoreWebApi.Extensions;
using ASP.NetCoreWebApi.src.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;
using System.Text;

string connectionString = "MicrosoftSQLSConnection";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataProtection()
    .SetApplicationName("AspNetCoreWebApi")
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\DataProtectionKeys"));

// Add services to the container.
builder.Services.AddHttpContextAccessor()
    .AddDistributedMemoryCache()
    .AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    })
    .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllersWithViews();


// Configure Database
builder.Services.AddDbContext<EF_DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString)));

builder.Services.AddAutoMapper(typeof(MappingProfile));


// Ilagay ito pagkatapos ng `builder.Services` configs
builder.Services.AddCustomServices();


// Configure Authentication and Authorization

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidateLifetime = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});

builder.Services.AddAuthorization(/*options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    // Add more policies for other roles if needed
}*/);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP NET Core Web APi", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy => policy.WithOrigins("http://localhost:5112")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS middleware
app.UseCors("AllowBlazorClient");

app.UseMiddleware<ExceptionHandlerMiddlewares>(); // Exception Handler

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<PaginationMiddleware>();
app.UseCookiePolicy();
app.MapControllers();
app.Run();
