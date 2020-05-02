using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.Application
{
    [Abp.Authorization.AbpAuthorize]
    [Frame.Common.CustomRoute(Frame.Common.ApiVersions.v1)]
    public class FrameApplicationBase : ApplicationService
    {
    }
}
