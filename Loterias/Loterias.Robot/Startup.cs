using Hangfire;
using Hangfire.MemoryStorage;
using Loterias.Application.Models;
using Loterias.Application.Services;
using Loterias.Application.Services.Interfaces;
using Loterias.Application.Utils.Csv.Models.Maps;
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
            services.AddTransient<IMessageService<Email>, EmailService>();
            services.AddTransient<IMessageProducerService, RabbitMQProducerService>();
            services.AddTransient<CsvMap>();
            services.AddSingleton<HttpClient>();

            services.Configure<GameRequestSettings>(
                Configuration.GetSection(nameof(GameRequestSettings)));

            services.Configure<TableSettings>(
                Configuration.GetSection(nameof(TableSettings)));

            services.Configure<EmailSendingSettings>(
                Configuration.GetSection(nameof(EmailSendingSettings)));

            services.Configure<RabbitMqSettings>(
                Configuration.GetSection(nameof(RabbitMqSettings)));

            services.Configure<GameSettings>(
                Configuration.GetSection(nameof(GameSettings)));

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
