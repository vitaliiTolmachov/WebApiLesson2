﻿using System.IO;
using System.Net;
using ApiRepository;
using Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CategoriesAndDepartmentsAPI
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5002);
                })
                .UseIISIntegration()
                .Build();

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            // Only used by EF Tooling
            return WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration((ctx, cfg) =>
                {
                    cfg.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true) // require the json file!
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((ctx, logging) => { }) // No logging
                .UseStartup<Startup>()
                .UseSetting("DesignTime", "true")
                .Build();
        }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseCategoryRepository();
            services.UseDepartmentRepository();
            services.AddSingleton(_ => Configuration);
            services.AddMvc().AddJsonOptions(options =>
                options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<ApiDbContext>((serviceProvider, dbOptionsBuilder) =>
            {
                dbOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("ProductApi"));
                dbOptionsBuilder.UseInternalServiceProvider(serviceProvider);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var err = JsonConvert.SerializeObject(new Error()
                        {
                            Message = ex.Error.Message,
                            Source = ex.Error.Source,
                            StackTrace = ex.Error.StackTrace
                        });

                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
            });

            app.UseDefaultFiles(); // For index.html
            app.UseStaticFiles(); // For the wwwroot folder

            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Department", action = "Get" });
            });
        }
    }
}
