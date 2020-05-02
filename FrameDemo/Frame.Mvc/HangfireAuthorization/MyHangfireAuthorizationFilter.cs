using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using Frame.Domain;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Frame.Application;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Auditing;

namespace Frame.Mvc
{
    public class MyHangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public IClientInfoProvider ClientInfoProvider { get; set; }
        public MyHangfireAuthorizationFilter()
        {
            ClientInfoProvider = NullClientInfoProvider.Instance;
        }



        public bool Authorize([NotNull] DashboardContext context)
        {
            return Verification(context);
        }

        private bool Verification(DashboardContext context)
        {
            if (context.Request.LocalIpAddress.Equals("127.0.0.1") || context.Request.LocalIpAddress.Equals("::1"))
                return true;
            var httpContext = context.GetHttpContext();
            var header = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(header))
                return SetChallengeResponse(httpContext);

            var authValues = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(header);

            if (!"Basic".Equals(authValues.Scheme, StringComparison.InvariantCultureIgnoreCase))
                return SetChallengeResponse(httpContext);

            var parameter = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
            var parts = parameter.Split(':');
            if (parts.Length < 2)
                return SetChallengeResponse(httpContext);

            var username = parts[0];
            var password = parts[1];

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return SetChallengeResponse(httpContext);
            if (!CheckPermission(context, username, password))
                return SetChallengeResponse(httpContext);
            return true;
        }

        public bool CheckPermission(DashboardContext context, string userName, string pwd)
        {
            var name = Frame.Common.AppConfigurationServices.Configuration["BackgroundServer:Hangfire:Dashboard:UserName"];
            var password = Frame.Common.AppConfigurationServices.Configuration["BackgroundServer:Hangfire:Dashboard:Pwd"];
            return (userName.Trim() == name && password == Frame.Common.EncryptDecode.GetMD5_32(pwd));
            //var service = IocManager.Instance.Resolve<ITestAppService>();
            //return await service.ContainPermission(context.Request.PathBase.ToLower(), userName, pwd);
        }

        private bool SetChallengeResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
            httpContext.Response.WriteAsync("Authentication is required.");
            return false;
        }
    }
}
