using DTTSG_BLL;
using DTTSG_BLL.Librarian;
using DTTSG_BLL.User;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using System;
using System.Web.Mvc;
using DTTSG_Common;
namespace DTTSG_Web.Controllers
{
    public class LoginController : Controller
    {
        UserLoginManager userLoginManager = new UserLoginManager();
        LibrarianLoginManager libLoginManager = new LibrarianLoginManager();
        NoticeManager noticeManager = new NoticeManager();

        [HttpGet]
        /// <summary>
        /// 跳转用户登录页
        /// </summary>
        /// <returns>带模型的视图</returns>
        public ActionResult Login()
        {
            UserInfo old = Session["User"] as UserInfo;
            if (old == null)
            {
                return View();
            }
            //检测用户退出
            if (Request["userlogout"] != null)
            {
                //手机端
                if (!string.IsNullOrWhiteSpace(old.OpenId))     //若openid不为空
                {
                    Session["User"] = userLoginManager.GetUserInfo(new UserInfo { OpenId = old.OpenId });
                    return Redirect("/Home/Index");
                }
                else
                {
                    Session.Remove("User");  //移除Session
                }
            }
            if (Request["liblogout"] != null)
            {
                Session.Remove("libUser");  //移除Session
            }

            //检测是否有用户在登录状态
            if (Session["User"] != null)
            {
                return Redirect("/Home/Index");
            }
            if (Session["libUser"] != null)
            {
                return Redirect("/Librarian/Home/Index");
            }

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
            // 转换模型成数据模型并与数据库比较
            UserInfo loginmodel = userLoginManager.GetUserInfo(new UserInfo()
            {
                UserId = (int)loginUser.UserId,
                UserPwd = loginUser.PassWord
            });
            // 检查可用状态且用户存在
            if (loginmodel != null)
            {
                if (loginmodel.StatuId != 1)    //检查用户状态
                {
                    return Json(new AjaxBackInfo(5, "您的账号异常,请联系管理员！"));
                }
                //成功登录进入主页
                loginmodel.OpenId = null;
                Session["User"] = loginmodel;    //设置Session状态

                return Json(new AjaxBackInfo(1, "欢迎您, " + loginmodel.UserName + " !"));
            }
            else
            {
                //图书管理员登录
                LibrarianInfo libinfo = libLoginManager.GetUserInfo(new LibrarianInfo()
                {
                    LibId = (int)loginUser.UserId,
                    LibPwd = loginUser.PassWord
                });
                if (libinfo != null)
                {
                    if (!libinfo.IsValid)    //检查用户状态
                    {
                        return Json(new AjaxBackInfo(5, "您的账号异常,请联系超级管理员！"));
                    }
                    //成功登录进入主页
                    Session["libUser"] = libinfo;    //设置Session状态
                    Notice notice = new Notice()
                    {
                        UserId = loginmodel.UserId,
                        LibId = 1001,
                        NoticeTitle = "地铁站登陆提醒",
                        NoticeContent = "您刚才通过地铁站点 登陆了我们的系统，祝您阅读愉快!",
                        NoticeTime = DateTime.Now,
                        N_TypeId = 5,

                    };

                    noticeManager.AddNotice(notice);

                    return Json(new AjaxBackInfo(2, "欢迎您, " + libinfo.LibName + " !"));
                }
                //管理员登录
                //暂时留位
                return Json(new AjaxBackInfo(4, "用户名或密码错误！"));
            }
        }


        public ActionResult OauthLogin(string openId)
        {
            UserInfo loginmodel = userLoginManager.GetUserInfo(new UserInfo { OpenId = openId });
            if (loginmodel != null)
            {
                if (loginmodel.StatuId != 1)    //检查用户状态
                {
                    return Json(new AjaxBackInfo(5, "您的账号异常,请联系管理员！"));
                }
                //成功登录进入主页
                Session["User"] = loginmodel;    //设置Session

                Notice notice = new Notice()
                {
                    UserId = loginmodel.UserId,
                    LibId = 1001,
                    NoticeTitle = "微信客户端登陆提醒",
                    NoticeContent = "您刚才通过微信客户端 登陆了我们的系统，祝您阅读愉快!",
                    NoticeTime = DateTime.Now,
                    N_TypeId = 5,

                };

                noticeManager.AddNotice(notice);

                return Redirect("/Home/Index");
            }
            else
            {
                return Json(new AjaxBackInfo(-1, "网络错误！！"));
            }


        }
    }
}