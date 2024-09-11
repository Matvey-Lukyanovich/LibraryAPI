using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using LibraryAPI.Application;
using LibraryAPI.Application.Common.Mappings;
using LibraryAPI.Application.Interfaces;
using LibraryAPI.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LibraryAPI.WebApi.Models;

namespace LibraryAPI.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Настройка сервисов
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // Регистрация TokenService
            builder.Services.AddScoped<TokenService>();

            // Настройка JWT аутентификации
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"]
                };
            });

            // Регистрация политик авторизации
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            // Регистрация ApplicationDbContext
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 21))));

            // Добавление Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library API",
                    Version = "v1",
                    Description = "An API for managing a library system",
                    Contact = new OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your.email@example.com",
                        Url = new Uri("https://yourwebsite.com")
                    }
                });
            });

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Использование Swagger в режиме разработки
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            // Применение политики CORS
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Включение аутентификации и авторизации
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}