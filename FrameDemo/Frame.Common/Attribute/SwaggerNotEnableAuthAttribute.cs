using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.Common
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SwaggerNotEnableAuthAttribute : Attribute
    {
        
    }
}
