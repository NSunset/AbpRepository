using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frame.Domain
{
    public enum StyleImageKindType
    {
        /// <summary>
        /// 款式
        /// </summary>
        [Description("款式")]
        Style = 1,

        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Img = 2
    }
}
