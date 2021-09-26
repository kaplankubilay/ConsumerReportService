using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumerReportService.ServiceImplementation;
using RabbitMQ.Client;
using RabbitMQManages.Connection;
using RabbitMQManages.Publisher;
using RabbitMQManages.Subscriber;

namespace ConsumerReportService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //RABBITMq CONFIGURATIONS
            //rabbitMq connection configuration
            services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://localhost"));
            //rabbitMq publisher configuration
            //services.AddSingleton<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
            //    "final_exchange",
            //    ExchangeType.Topic));
            services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
                "final_exchange",
                "report_queue",
                "report.*",
                ExchangeType.Topic));

            services.AddSingleton<IMemoryReportStorage, MemoryReportStorage>();
            services.AddHostedService<ReportDataCollector>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConsumerReportService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsumerReportService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
