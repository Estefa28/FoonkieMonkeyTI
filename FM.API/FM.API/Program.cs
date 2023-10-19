using FM.API.Configurations;
using FM.EntityFramework;
using FM.EntityFramework.Interfaces;
using FM.EntityFramework.Repositories;
using FM.External.API.Interfaces;
using FM.External.API.Implementation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Configuración de Documentación OpenAPI
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Configuración de Secciones appsettings.json
            builder.Services.Configure<Auth>(
                builder.Configuration.GetSection(Auth.AuthSection));

            // Configuración de Entity Framework
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"),
                sqloption =>
                {
                    sqloption.EnableRetryOnFailure(3);
                    sqloption.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                }));

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            // Inyección dependencias de API Externa
            builder.Services.AddSingleton<IUserAPI, UserAPI>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}