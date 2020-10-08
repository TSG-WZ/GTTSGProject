using Dapper;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_DAL
{
    public class UserInfoServer:BaseServer<UserInfo>
    {

        public UserInfo GetUserInfoWithOpenId(string openId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@OpenId", openId);
            return GetModel(@"select * from UserInfo where OpenId = @OpenId", parameters);

        }
    }
}
