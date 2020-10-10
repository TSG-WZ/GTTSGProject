using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_BLL.User;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using DTTSG_Web.Filts;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class UserController : Controller
    {
        UserInfoManager userInfoManager = new UserInfoManager();

        // GET: User
        public ActionResult ModifyPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyPassword(string old_password, string new_password, string again_password)
        {
            AjaxBackInfo info = null;
            if (new_password != again_password)     //检查重复
            {
                info = new AjaxBackInfo(1, "两次输入的密码不一致");
                return Json(info);
            }
            UserInfo newInfo = Session["User"] as UserInfo;
            if (old_password!=newInfo.UserPwd)
            {
                info = new AjaxBackInfo(1, "旧密码错误");
                return Json(info);
            }
           
            //修改数据库信息
            int result = userInfoManager.UpdatePwd(new_password,newInfo.UserId);
            if (result>0)
            {
                info = new AjaxBackInfo(1, "修改成功");
            }
            else
            {
                info = new AjaxBackInfo(1, "修改失败");
            }
            return Json(info);
        }
    }
}