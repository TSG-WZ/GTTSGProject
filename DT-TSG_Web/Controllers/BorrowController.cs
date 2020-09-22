using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_BLL.Book;
using DTTSG_DAL.Book;
using DTTSG_Model;

namespace DTTSG_Web.Controllers
{
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

        public ActionResult GetBookPagerData(BookInfo bookInfo, int pageindex = 1)
        {
            var userInfo = Session["User"] as UserInfo;
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = borrowManager.GetBorrowInfoList(pageindex, 8, userInfo); //固定八条数据
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
    }
}