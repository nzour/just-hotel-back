using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Root.Configuration
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.Headers.Add("Content-Type", "application/json");
                await context.Response.WriteAsync(CompileResponse(e));
            }
        }

        private string CompileResponse(Exception e)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            return e.InnerException != null
                ? CompileResponse(e.InnerException)
                : JsonConvert.SerializeObject(new ErrorResponse(e), settings);
        }
    }

    public class ErrorResponse
    {
        public ErrorResponse(Exception exception)
        {
            Type = exception.GetType().Name;
            Message = exception.Message;
        }

        public string Type { get; }
        public string Message { get; }
    }
}
