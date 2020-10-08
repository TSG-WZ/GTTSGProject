using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using DTTSG_Model;
using System.Data;
using DTTSG_Common;

namespace DTTSG_DAL
{
    public class UserLoginServer
    {
        /// <summary>
        /// 检查普通用户的登录方法
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(UserInfo userInfo)
        {
            UserInfo user = new UserInfo();
            string sql = @"select * from UserInfo us inner join UserType ut 
                                on us.TypeId=ut.TypeId inner join UserStatu ust 
                                on us.StatuId=ust.U_StatuId ";
            if (!string.IsNullOrWhiteSpace(userInfo.OpenId))
            {
                // 用OpenId  查询
                sql += "where us.OpenId=@OpenId and us.StatuId!=4";
            }
            else
            {
               sql += "where us.UserId=@UserId and us.UserPwd=@UserPwd and us.StatuId!=4";
            }
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    
                    user = connection.Query<UserInfo, UserType, UserStatu, UserInfo>
                        (sql, (us, ut, ust) => { us.UserType = ut; us.UserStatu = ust; return us; }
                        , userInfo, splitOn: "TypeId,U_StatuId").SingleOrDefault();
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
