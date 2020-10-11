using Dapper;
using DTTSG_Common;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DTTSG_DAL
{
    public class RankList
    {
        // 借阅排行
        public List<BookInfo> GetMaxBorrowBook(int count)// 最多的借阅书籍
        {
            string sql = @"select * from BookInfo where bookId in 
                        ( select  top "+count+@" BookId  from BorrowInfo 
                        group by BookId
                        ORDER BY count(BookId) DESC )  ";
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<BookInfo>(sql).ToList();
                };

            }
            catch (Exception ex)
            {

            }
            return null;

        }

        // 借阅书籍最多的人
        public List<UserInfo> GetMaxBorrowUser(int count)
        {
            string sql = @"select * from UserInfo where UserId in 
                        ( select  top  " + count + @" userId  from BorrowInfo 
                        group by UserId
                        ORDER BY count(UserId) DESC )  ";
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<UserInfo>(sql).ToList();
                };

            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public List<BorrowInfo> GetAllBorrowInfo()
        {
            DynamicParameters parameters = new DynamicParameters();
  

            string sql = @"select * from BorrowInfo br join BookInfo bi on br.BookId = bi.BookId 
                    join BorrowType brt on br.Bo_TypeId = brt.Bo_TypeId 
                    join MechanInfo me on bi.MechanId = me.MechanId 
                    join UserInfo ui on	ui.UserId = br.UserId ";
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<BorrowInfo, BookInfo, BorrowType, MechanInfo, UserInfo, BorrowInfo>(sql, (br, bi, brt, me, ui) =>
                    { br.BookInfo = bi; br.BorrowType = brt; br.MechanInfo = me; br.UserInfo = ui; return br; }
                        , parameters,
                        splitOn: "BookId,Bo_TypeId,MechanId,UserId").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

    }
}
