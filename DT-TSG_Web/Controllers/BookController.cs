using DTTSG_BLL.Book;
using DTTSG_DAL.Book;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class BookController : Controller
    {
        BookManager bookManager = new BookManager();
        BookTypeServer bookType = new BookTypeServer();

       
        public ActionResult BookList(BookInfo bookInfo)
        {
            #region 测试查询分页

            ViewBag.BookTypeList = bookType.GetBooKTypeList();
            //ViewBag.BookListPages = bookManager.GetBookList(bookInfo.B_TypeId, pageIndex, 8);

            #endregion

            return View();
        }

        //[HttpPost]
        /// <summary>
        /// 获取图书列表页
        /// </summary>
        /// <param name="bookInfo">图书信息(来自前端页面)</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns>返回视图</returns>
        public ActionResult GetBookPagerData(BookInfo bookInfo, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            var loginUserInfo = Session["User"] as UserInfo;
            jsonResult.Data = bookManager.GetBookList(bookInfo.B_TypeId, pageindex, 8, string.IsNullOrWhiteSpace(loginUserInfo.OpenId)); //固定八条数据
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }

        /// <summary>
        /// 图书详情页
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult BookInfo(int bookId)
        {
            BookInfo bookInfo = bookManager.GetBookModel(bookId);

            // 借书 ：
            // 1. 点击图书 =>显示图书详情 --> 传BookId =>返回BookInfo // Get请求

            return View(bookInfo);
        }
        [HttpPost]
        public ActionResult BorrowBook(int bookId)
        {
            // 2. 点击借书按钮  => 传BookId ，Session["UserId"] // POST请求，返回状态码，-1，0 1 2
            // 3. 根据状态码，提示借阅结果
            // 参数 ：书的Id， 
            var userinfo = Session["User"] as UserInfo;
            var result = bookManager.BookBrrow(bookId, userinfo);
            if (result == 1)
            {
                return Json(new AjaxBackInfo(1, "借阅成功"));
            }
            else if (result == 0)
            {
                return Json(new AjaxBackInfo(0, "借阅失败"));
            }
            else
            {
                return Json(new AjaxBackInfo(-1, "此书已借出！"));
            }

        }
        /// <summary>
        /// 还书
        /// 如果根据书的Id还，borrowId就传入0
        /// </summary>
        /// <param name="borrowId">根据借阅Id还</param>
        /// <param name="bookId">根据书Id还</param>
        /// <returns></returns>
        public ActionResult ReturnBook(int borrowId)
        {



            int result = bookManager.BookReturn(borrowId);

            if (result==1)
            {
                return Content("<script>alert('还书成功！');location.href = '/Borrow/BorrowList';</script>");
            }
            else
            {
                return Content("<script>alert('还书失败！联系管理员');</script>");
            }

            
        }
    }
}
