using System;
using System.Collections.Generic;

namespace app.Application.CQS.Token.Output
{
    public class TokenOutput
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}