using Microsoft.AspNetCore.Http;

using System;
using System.Threading.Tasks;

namespace AspNetCoreMiddleware
{
    public class ConsoleLoggerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Middle Before next");
            await next(context);
            Console.WriteLine("Middle After next");
        }
    }
}
