using DTTSG_BLL.Oauth;
using DTTSG_BLL.User;
using DTTSG_Common;
using DTTSG_Model;
using DTTSG_Model.DateModel;
using DTTSG_Model.POCOModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class WeiChatRequestController : Controller
    {

        OauthManager oauthManager = new OauthManager();
        UserInfoManager  userInfoManager = new UserInfoManager();
        // GET: WeiChatRequest
        public string Index(WeiChatRequestModel model)
        {
            if (model.Signature != null)
            {
                bool result = CheckSignature(model);
                if (result)
                {
                    return model.Echostr;
                }
                return string.Format("Signature:{0}\nTimestamp:{1}\nNonce:{2}\nResult:{3}",
                     model.Signature, model.Timestamp, model.Nonce, result.ToString());

            }
            else
            {
                return "model为空";
            }
        }

        private bool CheckSignature(WeiChatRequestModel model)
        {

            string timestamp = model.Timestamp;
            string nonce = model.Nonce;
            string token = "RTTSG";
            string[] array = { token, timestamp, nonce };
            Array.Sort(array);
            string tempStr = string.Join("", array);
            string resultStr = DataEncrypt.Sha1Sign(tempStr);
            if (resultStr.Equals(model.Signature))
            {
                return true;
            }

            return false;

        }
   
    
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        public ActionResult GetWinXinInfo(string code, string state)
        {         
            // 拿到第三方授权的用户信息；
            OauthUser oauthUserGet =  oauthManager.GetOUserInfo(code, state);
            if (string.IsNullOrWhiteSpace(oauthUserGet.OpenId))
            {
                return Content("用户信息获取失败");
            }
            // 去数据库查询该信息；
            OauthUser oauthUserFind = oauthManager.OautheCheck(oauthUserGet);
            
            if (oauthUserFind==null)
            {
                //数据库不存在该信息 注册
                //写入用户Oauth信息
                int oauthInsert = oauthManager.Insert(oauthUserGet);
                //                         
                int regResult = userInfoManager.OauthReg(oauthUserGet);
                if (regResult==0 || oauthInsert==0)
                {
                    // 注册用户出错
                    return Content("注册用户出错openid:"+oauthUserGet.OpenId);
                }
                
                
            }

            // 登录 用openId 找到该用户信息登录
            return RedirectToAction("OauthLogin", "Login", new { openId = oauthUserGet.OpenId });
            //return Content(string.Format("userinfo.openid:{0}userinfo.name:{1}",userInfo.OpenId,userInfo.UserName));

        }      

    }
}