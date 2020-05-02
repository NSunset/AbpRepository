using Frame.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Mvc.Controllers
{
    public class UploadController : MvcBaseController
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UploadController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public ActionResult Test()
        {
            return Json(new { code = 1, result = "测试" });
        }

        [HttpPost]
        [Frame.Common.CustomRoute(ApiVersions.v2,action:"haha")]
        public ActionResult Test2()
        {
            return Json(new { code = 1, result = "测试111" });
        }
    }
}
