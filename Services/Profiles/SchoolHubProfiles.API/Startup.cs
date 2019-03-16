using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolHub.Notification;
using SchoolHubProfiles.Application;
using SchoolHubProfiles.Core.Context;

namespace SchoolHubProfiles.API
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton(p => Configuration);

            // Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, p =>
                {
                    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            //Add DbContext
            services.AddDbContext<SchoolHubDbContext>(conn =>
                                      conn.UseSqlServer(Configuration.GetConnectionString("schoolHubConnectionString"), providerOptions => providerOptions.CommandTimeout(60))
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            //Add Mail
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddOptions();

            //Notification Services
            services.AddSchoolHubNotification();
            services.AddSchoolHubProfileApplication();
            services.AddSwaggerDocument();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors(_defaultCorsPolicyName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseCors(builder => builder.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()
                                    .AllowCredentials());

            app.UseHttpsRedirection();

            // Middleware  
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUi3();
            }
        }
    }
}
