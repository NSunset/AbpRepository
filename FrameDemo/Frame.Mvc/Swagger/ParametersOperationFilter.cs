using Frame.Common;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Mvc
{
    public class ParametersOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var notEnableAuthAttributes = context.ApiDescription.ActionAttributes().OfType<SwaggerNotEnableAuthAttribute>();
            if (!notEnableAuthAttributes.Any())
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "JWT Authorization header. 例如: \"Authorization: Bearer {token}\"",
                    Required = true,
                    Type = "string"
                });
            }
        }
    }
}
