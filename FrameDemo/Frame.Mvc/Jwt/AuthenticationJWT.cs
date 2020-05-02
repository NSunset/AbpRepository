using Abp.Runtime.Security;
using Frame.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Mvc
{
    public class AuthenticationJWT
    {

        public static void Config(IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(FrameCoreConsts.JwtIsEnable))
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = FrameCoreConsts.JwtAuthenticationScheme;
                    options.DefaultChallengeScheme = FrameCoreConsts.JwtAuthenticationScheme;
                }).AddJwtBearer(FrameCoreConsts.JwtAuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(FrameCoreConsts.JwtSignKey)),

                        ValidateIssuer = true,
                        ValidIssuer =FrameCoreConsts.JwtIssUer,

                        ValidateAudience = true,
                        ValidAudience =FrameCoreConsts.JwtAudience,

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = OnMessageReceived,
                        OnAuthenticationFailed = OnAuthenticationFailed,

                    };
                });

            }
        }

        /// <summary>
        /// 在首次接收到协议消息时调用。
        /// </summary>
        /// <param name="messageReceivedContext"></param>
        /// <returns></returns>
        private static Task OnMessageReceived(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue)

            {
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                // Cookie value does not matches to querystring value
                return Task.CompletedTask;
            }

            // Set auth token from cookie
            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken, "");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 在请求处理期间引发异常时调用。 例外情况
        /// 除非受到抑制，否则在此事件之后将被重新抛出
        /// </summary>
        /// <param name="authenticationFailedContext"></param>
        /// <returns></returns>
        private static Task OnAuthenticationFailed(AuthenticationFailedContext authenticationFailedContext)
        {
            if (authenticationFailedContext.Exception.GetType() == typeof(SecurityTokenException))
            {
                //捕捉令牌过期
                authenticationFailedContext.Response.Headers.Add("act", "expired");
            }
            return Task.CompletedTask;
        }

        
    }
}
