using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Frame.Mvc
{
    public class VersionControlDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var version = swaggerDoc.Info.Version;
            IDictionary<string, PathItem> paths = new Dictionary<string, PathItem>();
            var relativePaths = context.ApiDescriptions.Where(a => a.GroupName == version).Select(a =>a.RelativePath.TrimStart('/'));
            
            foreach (var item in swaggerDoc.Paths)
            {
                if (relativePaths.Contains(item.Key.TrimStart('/')))
                {
                    paths.Add(item.Key, item.Value);
                }
            }
            swaggerDoc.Paths = paths;

        }
    }
}
