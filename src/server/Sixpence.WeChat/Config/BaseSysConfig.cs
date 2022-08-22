using System;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;

namespace Sixpence.WeChat
{
    public class BaseSysConfig<T> where T : class
    {
        public static T Config { get; set; }
        static BaseSysConfig()
        {
            var em = EntityManagerFactory.GetManager();
            var sql = "SELECT * FROM sys_config WHERE code = @code";
            var param = new
            {
                code = EntityCommon.UpperChartToLowerUnderLine(typeof(T).Name.Replace("Config", ""))
            };
            Config = em.DbClient.QueryFirst<T>(sql, param);
        }
    }
}

