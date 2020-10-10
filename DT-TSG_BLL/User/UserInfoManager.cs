using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using DTTSG_Model.DateModel;
using DTTSG_Model.POCOModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_BLL.User
{
    public class UserInfoManager
    {
        UserInfoServer userInfoServer = new UserInfoServer();
        public int Update(UserInfo userInfo)
        {
            return userInfoServer.Update(userInfo);
        }

        public int UpdatePwd(string new_pwd,int userId)
        {
            new_pwd = new_pwd.GetMd5Hash();
            return userInfoServer.UpdatePwd(new_pwd,userId);
        }
        public int UserReg(UserInfo userInfo)
        {
            userInfo.UserPwd.GetMd5Hash();
            return userInfoServer.Insert(userInfo);
        }

        public int OauthReg(OauthUser oauthUser)
        {
            OauthUserData oUserData = JsonConvert.DeserializeObject<OauthUserData>(oauthUser.UserData);
            UserInfo userInfo = new UserInfo()
            {
                UserName = oUserData.nickname,
                UserSex = oUserData.sex == 1 ? true : false,
                UserAddress = oUserData.country + oUserData.city,
                UserPwd ="123456".GetMd5Hash(),
                StatuId = 1,
                TypeId = 1,
                ImageId = 1,
                OpenId = oauthUser.OpenId// 没有做外健

            };

            return userInfoServer.Insert(userInfo);
        }

       

    }
}
