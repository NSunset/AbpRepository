using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Frame.EntityFrameworkCore.MySql;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Configuration.Startup;
using Castle.Core.Internal;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Frame.Common;
using Microsoft.Extensions.Configuration;
using Frame.Domain;
using Microsoft.EntityFrameworkCore;

namespace Frame.EntityFrameworkCore
{
    [DependsOn(typeof(Abp.EntityFrameworkCore.AbpEntityFrameworkCoreModule),typeof(Frame.Common.FrameCommonModel), typeof(FrameDomainModel))]
    public class FrameEFCoreModel:AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IConnectionStringResolver, MyConnectionStringResolver>();
            AddDbContext<NapManageDbContext>();
            AddDbContext<NapDbContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //if (Exist(AppConfigurationServices.Configuration.GetConnectionString(FrameCoreConsts.ConnectionNapManageDbName)))
            //{
            //    SeedHelp.SeedHostDb(IocManager);
            //}
            SeedHelp.CreateDbIfNotExists(IocManager);
        }

        private void AddDbContext<T>() where T:AbpDbContext
        {
            Configuration.Modules.AbpEfCore().AddDbContext<T>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    MySqlDbContextConfiguration.Configure<T>(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    MySqlDbContextConfiguration.Configure<T>(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public static bool Exist(string connectionString)
        {
            if (connectionString.IsNullOrEmpty())
            {
                //connectionString is null for unit tests
                return true;
            }

            using (DbConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }
    }
}
