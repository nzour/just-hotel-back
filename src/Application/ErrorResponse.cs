using System;

namespace app.Application
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public ErrorResponse()
        {
            Type = "Uninitialized";
            Message = "No message";
        }

        public ErrorResponse(Exception e)
        {
            Type = e.GetType().Name;
            Message = e.Message;
        }
    }
}