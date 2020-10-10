using Dapper;
using DTTSG_Model.DateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_DAL
{
    public class OauthServer:BaseServer<OauthUser>
    {
        public OauthUser GetWithOpenId(string openId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@OpenId", openId);
            return GetModel(@"select * from OauthUser where OpenId = @OpenId",parameters);
        } 

        public override int Insert(OauthUser oauthUser)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@OpenId", oauthUser.OpenId);
            parameters.Add("@AccessToken", oauthUser.AccessToken);
            parameters.Add("@ExpiresIn", oauthUser.ExpiresIn);
            parameters.Add("@RefreshToken", oauthUser.RefreshToken);
            parameters.Add("@Scope", oauthUser.Scope);
            parameters.Add("@UserData", oauthUser.UserData);
          
            return Execute(@"INSERT INTO OauthUser(OpenId, AccessToken, ExpiresIn, RefreshToken, Scope, UserData)
                        VALUES (@OpenId,@AccessToken,@ExpiresIn,@RefreshToken,@Scope,@UserData)", parameters);
        }


    }
}
