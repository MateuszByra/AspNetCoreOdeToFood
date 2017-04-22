﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder() // configuration builder to add configuration file (appesettings.json here).
                                .SetBasePath(env.ContentRootPath) // use root path from hosting environment
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();
                Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter,Greeter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions{
                    ExceptionHandler=context=>context.Response.WriteAsync("Opps! It isn't Development environment.")
                });
            }

            app.UseFileServer();// use files in respond, eg. html.

            app.UseWelcomePage(new WelcomePageOptions{
                Path="/welcome"
            });


            app.Run(async (context) =>
            {
                var message = greeter.GetGreeting();
                await context.Response.WriteAsync(message);
            });
        }
    }
}
