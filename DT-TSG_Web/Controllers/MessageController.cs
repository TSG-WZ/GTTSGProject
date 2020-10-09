using DTTSG_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MessageManager messageManager = new MessageManager();
        public ActionResult MessageList()
        {
           ViewBag.MessageList= messageManager.GetMessageList(isValid:1); // 没有分页  isValid=1 表示 查询没有禁用的，默认查询所有的
            //Viewbag.messagelist = messageManager.GetMessagePagerList(0,pageSize:pageSize,isValid: 1);// 有分页
            return View();
        }
    }
}