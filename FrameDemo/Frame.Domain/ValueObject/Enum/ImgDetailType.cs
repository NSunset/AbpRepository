using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Frame.Domain
{
    public enum ImgDetailType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        Img=0,

        /// <summary>
        /// 款式-展示图
        /// </summary>
        [Description("款式-展示图")]
        Plan=1,

        /// <summary>
        /// 款式-单图
        /// </summary>
        [Description("款式-单图")]
        SingleImage=2
    }
}
