using System.Threading.Tasks;
using app.Common.Services.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace app.Aspect.FilterAttribute
{
    public class JsonResultFilterAttribute : ResultFilterAttribute, IGlobalFilter
    {
        public JwtTokenManager TokenManager { get; }

        public JsonResultFilterAttribute(JwtTokenManager tokenManager)
        {
            TokenManager = tokenManager;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var value = ExtractValue(context.Result);

            if (null == value)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            ReturnValue(context, value);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            // noop
        }

        /// <summary>
        /// Рефлексивно дернет приватное свойство Value из IActionResult
        /// </summary>
        private object ExtractValue(IActionResult result)
        {
            return result.GetType().GetProperty("Value")?.GetValue(result);
        }

        /// <summary>
        /// Сериализует value в JSON строку и вернет в качестве Response. 
        /// </summary>
        private void ReturnValue(ResultExecutingContext context, object value)
        {
            context.HttpContext.Response.OnStarting(state =>
            {
                context.HttpContext.Response.Headers.Add("Content-Type", "application/json");
                context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                (state as HttpContext)?.Response.WriteAsync(JsonConvert.SerializeObject(value));
                return Task.FromResult(0);
            }, context);
        }
    }
}