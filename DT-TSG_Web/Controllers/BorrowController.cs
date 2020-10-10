using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_BLL;
using DTTSG_Model;
using DTTSG_Web.Filts;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class BorrowController : Controller
    {
        BorrowManager borrowManager = new BorrowManager();
        
        // GET: Borrow
        public ActionResult BorrowList()
        {
            var userInfo = Session["User"] as UserInfo;
            ViewBag.BorrowInfoPagers = borrowManager.GetBorrowInfoList(1,8,userInfo);
           
            return View();
        }

        public ActionResult GetBorrowPagerData(BookInfo bookInfo, int pageIndex = 1, int pageSize = 10)
        {
            var userInfo = Session["User"] as UserInfo;
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = borrowManager.GetBorrowInfoList(pageIndex, pageSize, userInfo); //固定八条数据
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
    }
}