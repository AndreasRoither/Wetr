using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wetr.Web.Responses
{
    public class TokenResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new TokenResponse()
            {
                Token= "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9",
            };
        }
    }
}