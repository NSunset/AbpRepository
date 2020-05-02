using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Frame.Domain
{
    public interface ISqlExecuter
    {
        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int Execute(string sql, params object[] parameters);

        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql);
    }
}
