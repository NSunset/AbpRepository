using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using Frame.Common;
using Frame.Domain;
using Microsoft.Extensions.Configuration;

namespace Frame.EntityFrameworkCore.MySql
{
    public class MyConnectionStringResolver : DefaultConnectionStringResolver
    {
        public MyConnectionStringResolver(IAbpStartupConfiguration configuration) : base(configuration)
        {
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            if (args["DbContextConcreteType"] as Type==typeof(NapManageDbContext))
            {
                return AppConfigurationServices.Configuration.GetConnectionString(FrameCoreConsts.ConnectionNapManageDbName);
            }
            if (args["DbContextConcreteType"] as Type==typeof(NapDbContext))
            {
                return AppConfigurationServices.Configuration.GetConnectionString(FrameCoreConsts.ConnectionNapDbName);
            }
            return base.GetNameOrConnectionString(args);
        }
    }
}
