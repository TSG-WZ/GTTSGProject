using DTTSG_DAL;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTTSG_Common;
namespace DTTSG_BLL.User
{
    public class UserLoginManager
    {
        UserLoginServer userLoginServer = new UserLoginServer();

        /// <summary>
        /// 检查普通用户的登录方法
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(UserInfo userInfo)
        {
            if (!string.IsNullOrWhiteSpace(userInfo.UserPwd))
            {
                userInfo.UserPwd = userInfo.UserPwd.GetMd5Hash();
            }
            
            return userLoginServer.GetUserInfo(userInfo);
        }
    }
}
