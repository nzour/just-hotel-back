using app.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.Exceptions;

namespace app.Aspect.FilterAttribute
{
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute, IGlobalFilter
    {
        public override void OnException(ExceptionContext context)
        {
            var e = context.Exception;
            
            if (e is GenericADOException)
            {
                context.Exception = e.InnerException;
            }
            
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
            
            context.HttpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(new ErrorResponse(context.Exception))
            );
            
            context.ExceptionHandled = true;
        }
    }
}