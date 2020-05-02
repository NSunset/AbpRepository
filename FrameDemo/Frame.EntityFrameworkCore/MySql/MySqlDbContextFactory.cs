using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Frame.Common;
using Microsoft.Extensions.Configuration;
using Frame.Domain;
using System.Configuration;

namespace Frame.EntityFrameworkCore.MySql
{
    /// <summary>
    /// EFCore数据迁移创建Db
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MySqlDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public abstract string ConnectionStringName { get; }

        public abstract Func<DbContextOptionsBuilder,T> DbContext { get; }

        public T CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<T>();
            MySqlDbContextConfiguration.Configure<T>(builder, AppConfigurationServices.Configuration.GetConnectionString(ConnectionStringName));
            return DbContext(builder);
        }
    }

    public class NapManageFactory : MySqlDbContextFactory<NapManageDbContext>
    {

        public override string ConnectionStringName => FrameCoreConsts.ConnectionNapManageDbName;

        

        public override Func<DbContextOptionsBuilder, NapManageDbContext> DbContext => builder => { return new NapManageDbContext(builder.Options); };
    }

    public class NapFactory : MySqlDbContextFactory<NapDbContext>
    {
        public override string ConnectionStringName => FrameCoreConsts.ConnectionNapDbName;

        public override Func<DbContextOptionsBuilder, NapDbContext> DbContext => builder => { return new NapDbContext(builder.Options); };
    }
}
