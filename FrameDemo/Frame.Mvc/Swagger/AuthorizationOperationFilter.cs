using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Frame.Mvc
{
    /// <summary>
    /// 接口需要验证时加上验证失败的返回信息
    /// </summary>
    public class AuthorizationOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var controllerAttrs = context.ApiDescription.ControllerAttributes().OfType<Abp.Authorization.IAbpAllowAnonymousAttribute>();
            if (controllerAttrs.Any())
                return;
            var actionAttrs = context.ApiDescription.ActionAttributes().OfType<Abp.Authorization.IAbpAuthorizeAttribute>();           
            if (actionAttrs.Any())
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                var permissions = actionAttrs.SelectMany(p => p.Permissions).Distinct();

                if (permissions.Any())
                {
                    operation.Responses.Add("403", new Response { Description = "Forbidden" });
                }

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { "bearerAuth", permissions }
                    }
                };
            }
        }
    }
}
