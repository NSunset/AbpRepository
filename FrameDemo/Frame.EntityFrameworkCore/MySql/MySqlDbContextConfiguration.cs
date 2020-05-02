using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Frame.EntityFrameworkCore.MySql
{
    public static class MySqlDbContextConfiguration
    {
        public static void Configure<T>(DbContextOptionsBuilder<T> builder, string connectionString) where T : DbContext
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure<T>(DbContextOptionsBuilder<T> builder, DbConnection connection) where T : DbContext
        {
            builder.UseMySql(connection);
        }
    }
}
