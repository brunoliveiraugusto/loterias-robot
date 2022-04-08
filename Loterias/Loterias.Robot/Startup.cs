using Hangfire;
using Hangfire.MemoryStorage;
using Loterias.Application.Services;
using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Request;
using Loterias.Application.Utils.Request.Interfaces;
using Loterias.Application.Utils.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Loterias.Robot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMainService, MainService>();
            services.AddTransient<ICsvService, CsvService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IScrapingService, ScrapingService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddSingleton<HttpClient>();

            services.Configure<GameRequest>(
                Configuration.GetSection(nameof(GameRequest)));

            services.Configure<TablePosition>(
                Configuration.GetSection(nameof(TablePosition)));

            services.AddHangfire(op =>
            {
                op.UseMemoryStorage();
            });

            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<IMainService>(managerService => managerService.Execute(), Cron.Daily);
        }
    }
}
