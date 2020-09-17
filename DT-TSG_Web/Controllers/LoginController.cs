using DTTSG_BLL.User;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using RT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class LoginController : Controller
    {
        UserLoginManager userLoginManager = new UserLoginManager();

        [HttpGet]
        /// <summary>
        /// 跳转用户登录页
        /// </summary>
        /// <returns>带模型的视图</returns>
        public ActionResult Login()
        {
            if (Request["userlogout"] != null)
            {
                Session.Remove("User");  //移除Session
            }
            
            //检测是否有用户在登录状态
            if (Session["User"] != null)
            {
                return Redirect("/Home/Index");
            }
            
            //返回强类型视图模型(不返回前端密码报错!)
            return View();
        }

        [HttpPost]
        /// <summary>
        /// 检查登录信息
        /// </summary>
        /// <returns>Ajax结果</returns>
        public ActionResult LoginCheck(LoginModel loginUser)
        {
            //实体模型验证
            if (ModelState.IsValid)
            {
                return LoginInfoCheck(loginUser);
            }
            //人机 or 实体模型 验证未通过
            return Json(new AjaxBackInfo(0, "表单数据不合法！"));
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public JsonResult LoginInfoCheck(LoginModel loginUser)
        {
            //转换模型成数据模型并与数据库比较
            UserInfo loginmodel = userLoginManager.GetUserInfo(new UserInfo()
            {
                UserId = (int)loginUser.UserId,
                UserPwd = loginUser.PassWord
            });
            //检查可用状态且用户存在
            if (loginmodel != null)
            {
                if (!loginmodel.IsValid)    //检查用户状态
                {
                    return Json(new AjaxBackInfo(5, "您的账号已停用,请联系管理员！"));
                }
                //成功登录进入主页
                Session["User"] = loginmodel;    //设置Session状态
                return Json(new AjaxBackInfo(1, "欢迎您, " + loginmodel.UserName + " !"));
            }
            else
            {
                return Json(new AjaxBackInfo(4, "用户名或密码错误！"));
            }
        }
        


    }
}