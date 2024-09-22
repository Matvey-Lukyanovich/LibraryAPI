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
using LibraryAPI.Infrastructure.Repositories;
using MediatR;
using LibraryAPI.Application;
using MediatR;
using System.Reflection; // ��� ��������� ������
using LibraryAPI.Application.Auth.Commands;
using LibraryAPI.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using LibraryAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


namespace LibraryAPI.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

                var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // ��������� ��������
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            // ���������� ������������ �� appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



            // ����������� DbContext
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 25)))); // ������� ������ MySQL, ������� �����������


            // ��������� JWT ��������������
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
         };

     });



            // ����������� ������� �����������
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            // ����������� ApplicationDbContext
            builder.Services.AddDbContext<LibraryDbContext>(options =>
               options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                   new MySqlServerVersion(new Version(8, 0, 21))));



            // ���������� Swagger
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

            // ��������� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            
            // ����������� ITokenService
            builder.Services.AddScoped<ITokenService, TokenService>();

            // ����������� TokenService
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // ����������� MediatR ��� ������, ��� ��������� ������� � �����������
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LoginCommandHandler>());


            builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


 


            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));


                try
                {

                    var app = builder.Build();

            // ������������� Swagger � ������ ����������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            // ���������� �������� CORS
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // ��������� �������������� � �����������
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}