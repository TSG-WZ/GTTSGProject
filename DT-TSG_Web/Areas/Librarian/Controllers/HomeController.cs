using Library_BLL.Factory;
using Library_BLL.Interfaces;
using DTTSG_Common;
using DTTSG_Model;
using DTTSG_Web.Areas.Librarian.Filts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Areas.Librarian.Controllers
{
    /// <summary>
    /// 图书管理员首页控制器
    /// </summary>
    [LibrarianAutorizeAttribute]
    public class HomeController : Controller
    {
        ILibrarianManager iLibrarianManager = IoCUnity.Resolve<ILibrarianManager>();

        #region 图书管理员首页
        public ActionResult Index()
        {
            LibrarianInfo libUserInfo = Session["libUser"] as LibrarianInfo;
            //图书权限
            ViewBag.BookManage = iLibrarianManager.GetUserPowerInfo(libUserInfo, Config.PowerTypeInfo.BookManage);
            //借阅权限
            ViewBag.BorrowManage = iLibrarianManager.GetUserPowerInfo(libUserInfo, Config.PowerTypeInfo.BorrowManage);
            //通知权限
            ViewBag.NoticeMagage = iLibrarianManager.GetUserPowerInfo(libUserInfo, Config.PowerTypeInfo.NoticeMagage);
            //其他权限
            //ViewBag.OtherManage = iLibrarianManager.GetUserPowerInfo(libUserInfo, (int)Config.PowerTypeInfo.OtherManage);
            
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }

        #endregion

    }
}