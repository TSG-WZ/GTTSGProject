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

        public enum BookStatusList
        {
            正常=1,
            借阅中=2,
            损坏=3,
            丢失=4,
            售出=5
        };
    }
}
