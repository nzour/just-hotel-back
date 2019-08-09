using System;
using System.Threading.Tasks;
using app.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace app.Aspect
{
    public class TestMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               var task = next.Invoke(context);
               
               task.Wait();

               object result = null/*GetTaskResult(task)*/;

               // Not works
               if (null == result)
               {
                   return task;
               }
               
               context.Response.WriteAsync(JsonConvert.SerializeObject(result));
               return EmptyTask();

            }
            catch(AggregateException e)
            {
                context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse(e.InnerException)));
                return EmptyTask();
            }
        }
        
        private Task EmptyTask()
        {
            return new Task(() => {});
        }

        private object GetTaskResult(Task task)
        {
            var type = task.GetType();

            var property = type.GetProperty("Result");
            
            if (property == null)
            {
                return null;
            }
            
            return property.GetValue(task) ?? null;
        }
    }
}