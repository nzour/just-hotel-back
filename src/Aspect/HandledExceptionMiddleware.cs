using System;
using System.Threading.Tasks;
using app.Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace app.Aspect
{
    public class HandledExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var task = next.Invoke(context);

                task.Wait();

                await task;
            }
            catch(AggregateException e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.Headers.Add("Content-Type", "application/json");
                await context.Response.WriteAsync(CompileResponse(e));
            }
        }

        private string CompileResponse(Exception e)
        {
            return e.InnerException != null 
                ? CompileResponse(e.InnerException) 
                : JsonConvert.SerializeObject(new ErrorResponse(e));
        }
    }
}