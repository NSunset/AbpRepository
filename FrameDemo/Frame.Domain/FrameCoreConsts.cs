using Frame.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.Domain
{
    public class FrameCoreConsts
    {
        /// <summary>
        /// 跨域政策名
        /// </summary>
        public const string CrossDomainPolicyName = "Frame_AllowAll";

        #region 数据库连接配置

        /// <summary>
        /// NapManage 数据库连接配置名
        /// </summary>
        public const string ConnectionNapManageDbName = "Default";

        /// <summary>
        /// Nap 数据库连接配置名
        /// </summary>
        public const string ConnectionNapDbName = "Nap";

        #endregion

        #region 种子数据

        /// <summary>
        /// 管理员角色名
        /// </summary>
        public const string StaticRole_Admin = "管理员";

        /// <summary>
        /// 运营角色名
        /// </summary>
        public const string StaticRole_Operation = "运营人员";

        /// <summary>
        /// 财务角色名
        /// </summary>
        public const string StaticRole_Finance = "财务人员";

        /// <summary>
        /// 管理员用户名
        /// </summary>
        public const string StaticUser_Admin = "admin";

        #endregion

        #region Jwt配置

        /// <summary>
        /// JWT是否启用
        /// </summary>
        public static string JwtIsEnable = AppConfigurationServices.Configuration["Authentication:JwtBearer:IsEnable"];

        /// <summary>
        /// JWT默认方案名
        /// </summary>
        public const string JwtAuthenticationScheme = "JwtBearerTokenFrame";

        /// <summary>
        /// JWT签名密钥
        /// </summary>
        public static string JwtSignKey = AppConfigurationServices.Configuration["Authentication:JwtBearer:SigningKey"];

        /// <summary>
        /// JWT发行人
        /// </summary>
        public static string JwtIssUer = AppConfigurationServices.Configuration["Authentication:JwtBearer:Issuer"];

        /// <summary>
        /// JWT使用者
        /// </summary>
        public static string JwtAudience = AppConfigurationServices.Configuration["Authentication:JwtBearer:Audience"];

        /// <summary>
        /// JWT Token超时时间
        /// </summary>
        public static string JwtTokenTimeOutHours = AppConfigurationServices.Configuration["Authentication:JwtBearer:TokenTimeOutHours"];

        #endregion
    }
}
