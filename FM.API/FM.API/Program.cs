using FM.API.Configurations;
using FM.EntityFramework;
using FM.EntityFramework.Interfaces;
using FM.EntityFramework.Repositories;
using FM.External.API.Interfaces;
using FM.External.API.Implementation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FM.Business.Interfaces;
using FM.Business.Services;
using Microsoft.Extensions.Options;

namespace FM.API
{
    public class Program
    {
        private static System.Timers.Timer _timerExternalAPI;
        private static int _pageAPI = 1;
        private static IUserService _userService;
        private static Scheduler _scheduler;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Configuraci�n de Documentaci�n OpenAPI
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Configuraci�n de Secciones appsettings.json
            builder.Services.Configure<Auth>(
                builder.Configuration.GetSection(Auth.AuthSection));

            builder.Services.Configure<Scheduler>(
                builder.Configuration.GetSection(Scheduler.SchedulerSection));

            // Configuraci�n de Entity Framework
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"),
                sqloption =>
                {
                    sqloption.EnableRetryOnFailure(3);
                    sqloption.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                }));

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            // Inyecci�n dependencias de API Externa
            builder.Services.AddSingleton<IUserAPI, UserAPI>();

            // Inyecci�n dependencia capa Business
            builder.Services.AddTransient<IUserService, UserService>();

            var app = builder.Build();

            _userService = builder.Services.BuildServiceProvider().GetService<IUserService>();
            _scheduler = builder.Services.BuildServiceProvider().GetService<IOptions<Scheduler>>().Value;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            InitExternalAPI();

            app.Run();
        }

        /// <summary>
        /// Iniciar un Timer para ejecutar una tarea cada cierto tiempo
        /// </summary>
        private static void InitExternalAPI()
        {
            _timerExternalAPI = new System.Timers.Timer(_scheduler.Interval);
            _timerExternalAPI.Elapsed += Timer_Elapsed;
            _timerExternalAPI.Enabled = true;
        }

        /// <summary>
        /// Consulta cada p�gina trayendo los registros encontrados de la API Externa y los almacena en
        /// la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Console.WriteLine("Consultando");
                await _userService.UpdateDatabaseAsync(_pageAPI);
                Console.WriteLine($"Registros obtenidos de la pagina {_pageAPI}");
                _pageAPI++;
            }
            catch (Exception)
            {
                _timerExternalAPI.Enabled = false;
                Console.WriteLine("No hay m�s registros para consultar");
            }
        }
    }
}