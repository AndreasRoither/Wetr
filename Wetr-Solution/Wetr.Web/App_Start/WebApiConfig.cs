﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Wetr.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            config.MapHttpAttributeRoutes();

        }
    }
}
