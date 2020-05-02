using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Frame.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.EntityFrameworkCore.Uow;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Frame.EntityFrameworkCore
{
    public static class SeedHelp
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<NapManageDbContext>(iocResolver, SeedHostDb);
        }

        private static void SeedHostDb(NapManageDbContext context)
        {
            new RoleAndUserBuilder(context).Create();
        }


        private static void WithDbContext<T>(IIocResolver iocResolver,Action<T> context)where T: DbContext
        {
            using (var uowManage=iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow=uowManage.Object.Begin(System.Transactions.TransactionScopeOption.Suppress))
                {
                    var db = uowManage.Object.Current.GetDbContext<T>();
                    context(db);
                    uow.Complete();
                }
            }
        }

        public static void CreateDbIfNotExists(IWebHost host, IServiceCollection services)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<NapManageDbContext>();
                    context.Initialize();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

}
