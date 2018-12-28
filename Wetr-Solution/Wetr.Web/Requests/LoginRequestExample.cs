using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wetr.Web.Requests
{
    public class LoginRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new LoginRequest()
            {
                Email="daniel@wetr.net",
                Password="D5n13l"
            };
        }
    }
}