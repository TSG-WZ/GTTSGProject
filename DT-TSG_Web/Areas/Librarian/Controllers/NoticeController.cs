using Library_BLL.Factory;
using Library_BLL.Interfaces;
using Library_Model;
using Library_UI.Models;
using DTTSG_Web.Areas.Librarian.Filts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Areas.Librarian.Controllers
{
    [LibrarianAutorize]
    public class NoticeController : Controller
    {
        ILibrarianManager iLibrarianManager = IoCUnity.Resolve<ILibrarianManager>();

        #region 公告页面
        /// <summary>
        /// 公告列表页
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public ActionResult PublicNote(int pagesize = 10, int pageindex = 1)
        {
            ViewBag.NoticeList = iLibrarianManager.GetAllNoticeList(pagesize, pageindex);
            return View();
        }
        
        /// <summary>
        /// 搜索页面Json借阅列表(正常跳页和分页跳转)
        /// </summary>
        /// <param name="notify">公告信息</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns>Json对象</returns>
        public JsonResult GetNoticePagerData(NotifyModel notify, int pagesize = 10, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            if (notify.NotifyID != 0 || notify.Gid != 0 || notify.NotifyTitle != null)
                jsonResult.Data = iLibrarianManager.GetNoticeInfoById(notify, pagesize, pageindex);
            else
                jsonResult.Data = iLibrarianManager.GetAllNoticeList(pagesize, pageindex);
            return jsonResult;
        }

        /// <summary>
        /// 公告添加和修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult NoticeEdit(int? id)
        {
            ViewBag.noticeTypeList = GetSelectList();
            NotifyInfo notifyInfo = new NotifyInfo();
            if (id != null)//判断是添加还是修改
            {
                notifyInfo = (iLibrarianManager.GetNoticeInfoById(new NotifyModel() { NotifyID = (int)id }, 10, 1)).InfoList[0];
            }
            return View(notifyInfo);
        }

        /// <summary>
        /// 加载下拉列表
        /// </summary>
        /// <returns>下拉集合</returns>
        private List<SelectListItem> GetSelectList()
        {
            List<NotifyType> list = iLibrarianManager.GetNoticeTypeList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (NotifyType type in list)
            {
                SelectListItem item = new SelectListItem();
                item.Text = type.NotifyName;
                item.Value = type.NotifyTID.ToString();
                selectList.Add(item);
            }
            return selectList;
        }

        //[ValidateInput(false)]//取消验证
        /// <summary>
        /// 保存提交Ajax
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult NoticeSave(NotifyInfo model)
        {
            //修改还是添加
            if (model.NotifyID == 0)//添加
            {
                if (iLibrarianManager.IssueNotice(model,Session["libUser"] as LibrarianInfo))       //添加操作
                {
                    return Json(new AjaxBackInfo(1, "发布成功!"));
                }
            }
            else//修改
            {
                if (iLibrarianManager.ModifyNotice(model))       //修改操作
                {
                    return Json(new AjaxBackInfo(1, "修改成功!"));
                }
            }
            return Json(new AjaxBackInfo(2, "未知错误,请联系管理员!"));
        }

        /// <summary>
        /// 删除公告(逻辑)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult NoticeDelete(int id)
        {
            if (iLibrarianManager.NoticeDelete(new NotifyInfo() {NotifyID=id }))    //删除代码
            {
                return Json(new AjaxBackInfo(1, "删除成功!"));
            }
            return Json(new AjaxBackInfo(2, "删除失败,请联系管理员!"));
        }
        #endregion

        #region 点对点公告
        /// <summary>
        /// 点对点列表页
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns></returns>
        public ActionResult PrivateNote(int pagesize = 10, int pageindex = 1)
        {
            ViewBag.PrivateNoteList = iLibrarianManager.GetAllSNoticeList(pagesize, pageindex);
            return View();
        }

        /// <summary>
        /// 搜索页面Json借阅列表(正常跳页和分页跳转)
        /// </summary>
        /// <param name="notify">公告信息</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns>Json对象</returns>
        public JsonResult GetPrivateNotePagerData(SNotifyModel notify, int pagesize = 10, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            if (notify.SNotifyID != 0 || notify.Gid != 0 || notify.Sid != 0)
                jsonResult.Data = iLibrarianManager.GetSNoticeInfoById(notify, pagesize, pageindex);
            else
                jsonResult.Data = iLibrarianManager.GetAllSNoticeList(pagesize, pageindex);
            return jsonResult;
        }

        /// <summary>
        /// 公告添加和修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PrivateNoteEdit(int? id)
        {
            ViewBag.snoticeTypeList = GetPrivateNoteSelectList();
            SNotifyModel notifyInfo = new SNotifyModel();
            if (id != null)//判断是添加还是修改
            {
                notifyInfo = (iLibrarianManager.GetSNoticeInfoById(new SNotifyModel() { SNotifyID = (int)id }, 10, 1)).InfoList[0];
            }
            return View(notifyInfo);
        }

        /// <summary>
        /// 加载下拉列表
        /// </summary>
        /// <returns>下拉集合</returns>
        private List<SelectListItem> GetPrivateNoteSelectList()
        {
            List<SNotifyType> list = iLibrarianManager.GetSNoticeTypeList();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (SNotifyType type in list)
            {
                SelectListItem item = new SelectListItem();
                item.Text = type.SNotifyName;
                item.Value = type.SNotifyTID.ToString();
                selectList.Add(item);
            }
            return selectList;
        }

        //[ValidateInput(false)]//取消验证
        /// <summary>
        /// 保存提交Ajax
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult PrivateNoteSave(StuNotifyInfo model)
        {
            //修改还是添加
            if (model.SNotifyID == 0)//添加
            {
                if (iLibrarianManager.PtoPNotice(model, Session["libUser"] as LibrarianInfo))       //添加操作
                {
                    return Json(new AjaxBackInfo(1, "发布成功!"));
                }
            }
            else//修改
            {
                if (iLibrarianManager.ModifySNotice(model))       //修改操作
                {
                    return Json(new AjaxBackInfo(1, "修改成功!"));
                }
            }
            return Json(new AjaxBackInfo(2, "未知错误,请联系管理员!"));
        }

        /// <summary>
        /// 删除公告(逻辑)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult PrivateNoteDelete(int id)
        {
            if (iLibrarianManager.NoticeDelete(new NotifyInfo() { NotifyID = id }))    //删除代码
            {
                return Json(new AjaxBackInfo(1, "删除成功!"));
            }
            return Json(new AjaxBackInfo(2, "删除失败,请联系管理员!"));
        }
        #endregion
    }
}