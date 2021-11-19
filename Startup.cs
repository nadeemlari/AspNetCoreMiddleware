using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace AspNetCoreMiddleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ConsoleLoggerMiddleware>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.Map("/map", HandlerMap);
            app.UseWhen(context => context.Request.Query.ContainsKey("p"), MapWhenHandler);
            app.UseMiddleware<ConsoleLoggerMiddleware>();
            app.Run(async context =>
            {
                Console.WriteLine("Hello world");
                await context.Response.WriteAsync("Hello world");
            });


        }

        private void MapWhenHandler(IApplicationBuilder app)
        {
            app.Use( async (context, next) =>
            {
                Console.WriteLine("Hello from MapWhenHandler");
                await next();
            });
        }

        private void HandlerMap(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                Console.WriteLine("Hello from Map");
                await context.Response.WriteAsync("Hello from map");
            });
       
        }
    }
}
