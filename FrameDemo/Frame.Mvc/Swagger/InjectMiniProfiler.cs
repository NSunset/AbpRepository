using Microsoft.AspNetCore.Http;
using StackExchange.Profiling;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Mvc
{
    public class InjectMiniProfiler : IDocumentFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public InjectMiniProfiler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Info.Contact = new Contact
            {
                Name = MiniProfiler.Current.RenderIncludes(httpContextAccessor.HttpContext).ToString()
            };
        }
    }
}
