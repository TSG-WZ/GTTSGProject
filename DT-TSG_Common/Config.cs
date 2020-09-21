using System.Configuration;
using System.Net;

namespace DTTSG_Common
{
    public class Config
    {
        /// <summary>
        /// 读取数据库连接地址
        /// </summary>
        public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        /// <summary>
        /// 机器码
        /// </summary>
        public static string GetHostName = Dns.GetHostName();

    }
}
