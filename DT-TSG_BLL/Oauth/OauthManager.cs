
using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using DTTSG_Model.DateModel;
using Newtonsoft.Json;

namespace DTTSG_BLL.Oauth
{
    public class OauthManager
    {
        private string appSecret =Config.AppSecret;
        private string appId =Config.AppId;

        OauthServer oauthServer = new OauthServer();
        UserInfoServer userInfoServer = new UserInfoServer();
        NoticeServer noticeServer = new NoticeServer();

        public OauthUser GetOUserInfo(string code, string state)
        {
            RequestExtension res = new RequestExtension();
            //获取AccessToken
            string getAccessTokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appId +
              "&secret=" + appSecret + "&code=" + code + "&grant_type=authorization_code";
            string getAccessTokenReponse = res.GetInfo(getAccessTokenUrl);
            WeiXinResponse modeAccessToken = JsonConvert.DeserializeObject<WeiXinResponse>(getAccessTokenReponse);

            //获取用户信息
            string getUserInfoUrl = "https://api.weixin.qq.com/sns/userinfo?access_token=" + modeAccessToken.access_token
                + "&openid=" + modeAccessToken.openid + "&lang=zh_CN";
            string getUserInfoReponse = res.GetInfo(getUserInfoUrl);

            OauthUser oauthUser = new OauthUser
            {
                UserData = getUserInfoReponse,
                OpenId = modeAccessToken.openid,
                ExpiresIn = modeAccessToken.expires_in,
                RefreshToken = modeAccessToken.refresh_token,
                Scope = modeAccessToken.scope,
                AccessToken = modeAccessToken.access_token

            };
            return oauthUser;

        }

        /// <summary>
        /// 返回数据库中与有的OauthUser
        /// </summary>
        /// <param name="oauthUser">第三方认证所取得的用户信息</param>
        /// <returns></returns>
        public OauthUser OautheCheck(OauthUser oauthUser)
        {
            return oauthServer.GetWithOpenId(oauthUser.OpenId);

        }

        public int Insert(OauthUser oauthUser)
        {
            
            return oauthServer.Insert(oauthUser);
        }

        private class WeiXinResponse
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string openid { get; set; }
            public string scope { get; set; }

        }
    }
}
