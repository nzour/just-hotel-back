using System;

namespace app.Application.Middleware
{
    public class ErrorResponse
    {
        public string Type { get; }
        public string Message { get; }

        public ErrorResponse()
        {
            Type = "Uninitialized";
            Message = "No message";
        }

        public ErrorResponse(Exception exception)
        {
            Type = exception.GetType().Name;
            Message = exception.Message;
        }
    }
}