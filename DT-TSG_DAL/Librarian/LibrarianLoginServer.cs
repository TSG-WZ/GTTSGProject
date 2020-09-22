using Dapper;
using DTTSG_Common;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_DAL.Librarian
{
    public class LibrarianLoginServer
    {
        public LibrarianInfo GetUserInfo(LibrarianInfo userInfo)
        {
            LibrarianInfo user = new LibrarianInfo();
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    string sql = @"select * from LibrarianInfo where LibId=@LibId and LibPwd=@LibPwd and IsValid=1";
                    user = connection.Query<LibrarianInfo>(sql, userInfo).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return user;
        }
    }
}
