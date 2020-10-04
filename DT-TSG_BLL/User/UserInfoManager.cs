using DTTSG_Common;
using DTTSG_DAL.User;
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

        public int UserReg(UserInfo userInfo)
        {
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
                UserPwd = oUserData.nickname+"RTDT",
                StatuId = 1,
                TypeId = 1,
                ImageId = 1,
                OpenId = oauthUser.OpenId// 没有做外健

            };

            return userInfoServer.Insert(userInfo);
        }


    }
}
