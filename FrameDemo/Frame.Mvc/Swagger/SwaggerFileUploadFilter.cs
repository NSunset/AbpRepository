using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Mvc
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var files = context.ApiDescription.ActionDescriptor.Parameters.Where(a => a.ParameterType == typeof(IFormFile));
            if (files.Count()>0)
            {
                operation.Consumes.Add("multipart/form-data");
                foreach (var item in files)
                {
                    var parameter = operation.Parameters.Single(a => a.Name == item.Name);
                    operation.Parameters.Remove(parameter);
                    operation.Parameters.Add(new NonBodyParameter
                    {
                        Name = item.Name,
                        Type = "file",
                        In = "formData",
                        Description = parameter.Description,
                        Required = parameter.Required
                    });
                }
            }
            
        }
    }
}
