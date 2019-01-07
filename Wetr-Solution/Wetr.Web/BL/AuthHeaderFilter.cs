using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace Wetr.Web.BL
{
    /// Adapted from https://alexdunn.org/2018/06/29/adding-a-required-http-header-to-your-swagger-ui-with-swashbuckle/
    /// <summary>
    /// Operation filter to add the requirement of the authorization header
    /// </summary>
    public class AuthHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                type = "string",
                required = false
            });
        }

        
    }
}