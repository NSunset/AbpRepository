using Abp.EntityFrameworkCore;
using Frame.Domain;
using Frame.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frame.EntityFrameworkCore
{
    public class SqlExecuter : ISqlExecuter, Abp.Dependency.ITransientDependency
    {
        private readonly IDbContextProvider<NapManageDbContext> dbContextProvider;

        public SqlExecuter(IDbContextProvider<NapManageDbContext> dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        public int Execute(string sql, params object[] parameters)
        {
            return dbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
        }


        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql)
        {
            return await dbContextProvider.GetDbContext().Database.ExecuteSqlCommandAsync(sql);
        }
    }
}
