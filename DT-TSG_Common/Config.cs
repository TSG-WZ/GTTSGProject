using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_Common
{
    public class Config
    {
        /// <summary>
        /// 读取数据库连接地址
        /// </summary>
        public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    }
}
