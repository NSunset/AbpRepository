using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Frame.Domain;
using Frame.Common;
using Castle.Core.Logging;
using Hangfire;
using Hangfire.MySql.Core;
using Frame.BackgroundWorker.Hangfire;
using StackExchange.Profiling.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Abp.AspNetCore.SignalR.Hubs;
using Frame.EntityFrameworkCore;

namespace Frame.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc(options =>
            {
                options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
            });

            //swagger配置
            services.AddSwaggerGen(options =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    options.SwaggerDoc(version, new Swashbuckle.AspNetCore.Swagger.Info { Title = "Frame Api", Version = version });
                });
                options.DocInclusionPredicate((docName, description) => true);
                var baseDir = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;

                options.IncludeXmlComments(System.IO.Path.Combine(baseDir, "Frame.Application.xml"));
                options.IncludeXmlComments(System.IO.Path.Combine(baseDir, "Frame.Domain.xml"));
                options.IncludeXmlComments(System.IO.Path.Combine(baseDir, "Frame.Mvc.xml"));

                options.OperationFilter<AuthorizationOperationFilter>();
                options.OperationFilter<ParametersOperationFilter>();
                options.DocumentFilter<VersionControlDocumentFilter>();
                //options.DocumentFilter<InjectMiniProfiler>();
            });

            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy(FrameCoreConsts.CrossDomainPolicyName, p => p.SetIsOriginAllowed(a => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

            //signalR配置
            services.AddSignalR();

            //监控sql执行工具
            services.AddMiniProfiler(options =>
            {
                // All of this is optional. You can simply call .AddMiniProfiler() for all defaults

                // (Optional) Path to use for profiler URLs, default is /mini-profiler-resources
                options.RouteBasePath = "/profiler";

                // (Optional) Control storage
                // (default is 30 minutes in MemoryCacheStorage)
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);

                // (Optional) Control which SQL formatter to use, InlineFormatter is the default
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

                // (Optional) To control authorization, you can use the Func<HttpRequest, bool> options:
                // (default is everyone can access profilers)
                //options.ResultsAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;
                //options.ResultsListAuthorize = request => MyGetUserFunction(request).CanSeeMiniProfiler;

                // (Optional)  To control which requests are profiled, use the Func<HttpRequest, bool> option:
                // (default is everything should be profiled)
                //options.ShouldProfile = request => MyShouldThisBeProfiledFunction(request);

                // (Optional) Profiles are stored under a user ID, function to get it:
                // (default is null, since above methods don't use it by default)
                //options.UserIdProvider = request => MyGetUserIdFunction(request);

                // (Optional) Swap out the entire profiler provider, if you want
                // (default handles async and works fine for almost all applications)
                //options.ProfilerProvider = new MyProfilerProvider();

                // (Optional) You can disable "Connection Open()", "Connection Close()" (and async variant) tracking.
                // (defaults to true, and connection opening/closing is tracked)
                options.TrackConnectionOpenClose = true;
            }).AddEntityFramework();

            //JWT配置
            AuthenticationJWT.Config(services, Configuration);

            //Hangfire配置
            if (bool.Parse(Configuration["BackgroundServer:Hangfire:Enable"]))
            {
                services.AddHangfire(
                new MySqlStorage(Configuration.GetConnectionString(FrameCoreConsts.ConnectionNapManageDbName), new MySqlStorageOptions { InvisibilityTimeout = TimeSpan.FromMinutes(5) }),
                CompatibilityLevel.Version_170
                );
            }

            return services.AddAbp<FrameMvcModel>(optionsAction =>
            {
                //log4et配置
                if (!Abp.Dependency.IocManager.Instance.IsRegistered(typeof(ILoggerFactory)))
                {
                    optionsAction.IocManager.IocContainer.AddFacility<Castle.Facilities.Logging.LoggingFacility>(f => f.UseAbpLog4Net().WithConfig("log4net.config"));
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Hangfire配置
            if (bool.Parse(Configuration["BackgroundServer:Hangfire:Enable"]))
            {
                app.HangfireConfigure(pathMatch: Configuration["BackgroundServer:Hangfire:PathMatch"].ToString(),
               dashboardOptions: new DashboardOptions
               {
                   Authorization = new[]
                   {
                        //new AbpHangfireAuthorizationFilter(),
                        new MyHangfireAuthorizationFilter()
                   }
               });
            }



            app.UseAbp();

            //swagger配置
            if (bool.Parse(Configuration["Swagger:Enable"]))
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    typeof(ApiVersions).GetEnumNames().OrderByDescending(a => a).ToList().ForEach(version =>
                    {
                        options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Frame Api {version}");
                    });

                    //添加MiniProfiler  监视sql执行工具swagger配置
                    options.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("Frame.Mvc.wwwroot.swagger.index.html");
                    //options.RoutePrefix = "api";
                });
            }

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            //跨域配置
            app.UseCors(FrameCoreConsts.CrossDomainPolicyName);

            //signalR 配置
            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
                routes.MapHub<MyChantHub>("/myChatHub");
            });

            //监控sql执行工具配置
            if (bool.Parse(Configuration["MiniProfiler:Enable"]))
            {
                app.UseMiniProfiler();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
