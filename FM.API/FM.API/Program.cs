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

            // OpenAPI Documentation Configuration
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Section Settings appsettings.json
            builder.Services.Configure<Auth>(
                builder.Configuration.GetSection(Auth.AuthSection));

            builder.Services.Configure<Scheduler>(
                builder.Configuration.GetSection(Scheduler.SchedulerSection));

            // Entity Framework Configuration
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"),
                sqloption =>
                {
                    sqloption.EnableRetryOnFailure(3);
                    sqloption.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                }));

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            // External API dependency injection
            builder.Services.AddSingleton<IUserAPI, UserAPI>();

            // Business layer dependency injection
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
        /// Start a Timer to execute a task every so often
        /// </summary>
        private static void InitExternalAPI()
        {
            _timerExternalAPI = new System.Timers.Timer(_scheduler.Interval);
            _timerExternalAPI.Elapsed += Timer_Elapsed;
            _timerExternalAPI.Enabled = true;
        }

        /// <summary>
        /// Queries each page bringing the records found from the External API and stores them in
        /// the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Console.WriteLine("Fetching");
                await _userService.UpdateDatabaseAsync(_pageAPI);
                Console.WriteLine($"Records obtained from the page {_pageAPI}");
                _pageAPI++;
            }
            catch (Exception)
            {
                _timerExternalAPI.Enabled = false;
                Console.WriteLine("No records found");
            }
        }
    }
}