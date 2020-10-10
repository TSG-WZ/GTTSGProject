using System.Security.Cryptography;
using System.Text;

namespace DTTSG_Common
{
    /// <summary>
    /// EncyrptHeper类是用来加密密码的类，后期可以加入其他扩展加密算法
    /// 此类使用MD5加密 
    /// </summary>
    public static class EncyrptHelper
    {
        /// <summary>
        /// 此算法为MD5 不可解密  
        /// </summary>
        /// <param name="str">为确保安全 可将str在加密之前 再加一个只有您知道的字符串 传入此方法</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMd5Hash(this string str)
        {
            using (MD5 md5Obj = MD5.Create())//using 为了及时释放md5Obj对象
            {
                var md5Bytes = md5Obj.ComputeHash(Encoding.UTF8.GetBytes(str));//转成bytes 并且计算其哈希值，也就是加密算法
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < md5Bytes.Length; i++)
                {
                    builder.Append(md5Bytes[i].ToString("X2"));//转成 大写的MD5加密后的字符串，X2 必须写 转成大写的字母，x2 是转成小写的字母
                }
                return builder.ToString();
            }

        }
    }
}

