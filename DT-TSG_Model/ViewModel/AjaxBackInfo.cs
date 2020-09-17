using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTTSG_Model.ViewModel
{
    /// <summary>
    /// Ajax信息回调类
    /// </summary>
    public class AjaxBackInfo
    {
        /// <summary>
        /// 含参构造方法
        /// </summary>
        /// <param name="Status">状态码</param>
        /// <param name="Msg">消息</param>
        public AjaxBackInfo(int Status, string Msg)
        {
            this.Msg = Msg;
            this.Status = Status;
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}