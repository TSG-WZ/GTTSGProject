using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTTSG_Model.ViewModel
{
    /// <summary>
    /// 用户登录视图模型
    /// </summary>
    public class LoginModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入{0} !")]
        /// <summary>
        /// 用户名
        /// </summary>
        public int? UserId { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0} !")]
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 是否记住密码
        /// </summary>
        //public bool IsRemember { get; set; }
    }
}
