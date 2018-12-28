using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wetr.Web.Responses
{
    public class LoginResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new LoginResponse()
            {
                Token= "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOiJiMDhmODZhZi0zNWRhLTQ4ZjItOGZhYi1jZWYzOTA0NjYwYmQifQ.-xN_h82PHVTCMA9vdoHrcZxH-x5mb11y1537t3rGzcM"
            };
        }
    }
}