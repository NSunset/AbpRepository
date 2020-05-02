using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {


        public CustomRouteAttribute(string controller = "[controller]", string action = "[action]") : base("/api/" + $"{controller}/{action}")
        {

        }

        public CustomRouteAttribute(ApiVersions version, string controller = "[controller]", string action = "[action]") : base($"/api/{version.ToString()}/{controller}/{action}")
        {
            GroupName = version.ToString();
        }

        public string GroupName { get; private set; }
    }
}
