using DTTSG_BLL;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using DTTSG_Web.Areas.Librarian.Filts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Areas.Librarian.Controllers
{
    [LibrarianAutorizeAttribute]
    /// <summary>
    /// 图书管理员 图书管理控制器
    /// </summary>
    public class BookController : Controller
    {
        BookManager bookManager = new BookManager();

        /// <summary>
        /// 图书列表
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public ActionResult BookList(int pagesize = 10, int pageindex = 1)
        {
            //查询图书列表
            ViewBag.BookList = bookManager.GetBookList(0, pageindex, pagesize);
            return View();
        }

        public ActionResult GetBookPagerData(int pagesize = 10, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = bookManager.GetBookList(0, pageindex, pagesize); //固定八条数据
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }

        /// <summary>
        /// 图示添加和修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BookEdit(int? id)
        {
            ViewBag.bookTypeList = GetSelectList();
            BookInfo bookInfo = new BookInfo();
            if (id != null)//判断是添加还是修改
            {
                //bookInfo = (iStudentManager.GetBookInfoById(new BookInfo() { BookId = (int)id }, 10, 1)).InfoList[0];
            }
            return View(bookInfo);
        }

        /// <summary>
        /// 加载下拉列表
        /// </summary>
        /// <returns>下拉集合</returns>
        private List<SelectListItem> GetSelectList()
        {
            //List<BookType> list = iLibrarianManager.GetBookTypeList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            //foreach (BookType booktype in list)
            //{
            //    SelectListItem item = new SelectListItem();
            //    //item.Text = booktype.Tname;
            //    //item.Value = booktype.Ttype.ToString();
            //    selectList.Add(item);
            //}
            return selectList;
        }

        //[ValidateInput(false)]//取消验证
        /// <summary>
        /// 保存提交Ajax
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult ProductSave(BookInfo model)
        {
            //修改还是添加
            if (model.BookId == 0)//添加
            {
                //if (iLibrarianManager.AddBookInfo(model))       //添加成功
                //{
                //    return Json(new AjaxBackInfo(1, "添加成功!"));
                //}
            }
            else//修改
            {
                //if (iLibrarianManager.ModifyBookInfo(model))       //修改成功
                //{
                //    return Json(new AjaxBackInfo(1, "修改成功!"));
                //}
            }
            return Json(new AjaxBackInfo(2, "未知错误,请联系管理员!"));
        }

        /// <summary>
        /// 删除图书信息(逻辑)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ProductDelete(int id)
        {
            //if (iLibrarianManager.DelteBookInfo(new BookInfo() { BookId = id }))    //删除代码
            //{
            //    return Json(new AjaxBackInfo(1, "删除成功!"));
            //}
            return Json(new AjaxBackInfo(2, "删除失败,请联系管理员!"));
        }



    }
}