using DTTSG_DAL;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_BLL.Librarian
{
    public class LibrarianLoginManager
    {
        LibrarianLoginServer librarianLoginServer = new LibrarianLoginServer();

        /// <summary>
        /// 检查图书管理用户的登录方法
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public LibrarianInfo GetUserInfo(LibrarianInfo userInfo)
        {
            return librarianLoginServer.GetUserInfo(userInfo);
        }
    }
}
