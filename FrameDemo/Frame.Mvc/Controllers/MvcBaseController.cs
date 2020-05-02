using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Mvc.Controllers
{
    [Abp.Authorization.AbpAuthorize]
    [Frame.Common.CustomRoute(version: Frame.Common.ApiVersions.v1)]
    public abstract class MvcBaseController:Abp.AspNetCore.Mvc.Controllers.AbpController
    {
    }
}
