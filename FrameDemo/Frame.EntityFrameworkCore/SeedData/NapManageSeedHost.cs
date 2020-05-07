using Abp.Dependency;
using Frame.EntityFrameworkCore.MySql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frame.EntityFrameworkCore
{
    public class NapManageSeedHost
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            SeedHelp.WithDbContext<NapManageDbContext>(iocResolver, (NapManageDbContext context) =>
            {
                context.Initialize();
            });
        }

    }
}
