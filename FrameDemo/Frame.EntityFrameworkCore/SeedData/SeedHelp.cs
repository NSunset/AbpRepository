using Abp.Dependency;
using Abp.Domain.Uow;
using Frame.Common;
using Frame.Domain;
using Frame.EntityFrameworkCore.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Hosting;
using Abp.EntityFrameworkCore.Uow;

namespace Frame.EntityFrameworkCore
{
    public static class SeedHelp
    {
        public static void WithDbContext<T>(IIocResolver iocResolver,Action<T> context)where T: DbContext
        {
            using (var uowManage=iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow=uowManage.Object.Begin(System.Transactions.TransactionScopeOption.Suppress))
                {
                    try
                    {
                        var db = uowManage.Object.Current.GetDbContext<T>();
                        context(db);
                        uow.Complete();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    
                }
            }
        }

    }

}
