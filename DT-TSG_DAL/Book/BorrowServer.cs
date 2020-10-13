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
    public class BorrowServer : BaseServer<BorrowInfo>
    {
        public List<BorrowInfo> GetBorrowList(int userId=0,bool isPager=false ,int pageIndex=0, int pageSize=0 )
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pageIndex", pageIndex);
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@MechanName", Config.GetHostName);

            string sql = @"select * from BorrowInfo br join BookInfo bi on br.BookId = bi.BookId 
                    join BorrowType brt on br.Bo_TypeId = brt.Bo_TypeId 
                    join MechanInfo me on bi.MechanId = me.MechanId 
                    join UserInfo ui on	ui.UserId = br.UserId where 
                    1=1";
            if (userId!=0 )
            {
                parameters.Add("@UserId",userId);
                sql += " and ui.UserId = @UserId";
            }
            //加分页
            if (isPager)
            {
                sql += " order  by brt.Seo  desc offset(@pageIndex - 1) * @pageSize " +
                "rows fetch next @pageSize rows only";
            }
            
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<BorrowInfo, BookInfo, BorrowType, MechanInfo,UserInfo, BorrowInfo>(sql,(br, bi, brt, me, ui) =>
                    { br.BookInfo= bi; br.BorrowType = brt;br.MechanInfo= me; br.UserInfo = ui; return br; }
                        , parameters,
                        splitOn: "BookId,Bo_TypeId,MechanId,UserId").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public int GetBorrowListLength(int userId=0)
        {
            
            DynamicParameters parma = new DynamicParameters();
            parma.Add("@MechanName", Config.GetHostName);
          
            string sql = @"select * from BorrowInfo br join MechanInfo me on
                        br.B_MechanId = me.MechanId where
                        me.MechanName = @MechanName ";
           
            if (userId != 0)
            {
                parma.Add("@userId", userId);
                sql += " and br.userId = @userId";
            }
           return GetList(sql, parma).Count;
        }

        public BorrowInfo GetBorrowInfoModel(int borrowId,int bookId=0)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MechanName", Config.GetHostName);
           
            string sql = @"select * from BorrowInfo br join BookInfo bi on br.BookId = bi.BookId 
                    join BorrowType brt on br.Bo_TypeId = brt.Bo_TypeId 
                    join MechanInfo me on bi.MechanId = me.MechanId 
                    join UserInfo ui on	ui.UserId = br.UserId where 
                    me.MechanName = @MechanName ";
            parameters.Add("@Id", bookId==0?borrowId:bookId);
            if (bookId==0)
            {
                sql += "and br.borrowId = @Id";
            }
            else
            {
                sql += "and br.bookId = @Id";
            }
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<BorrowInfo, BookInfo, BorrowType, MechanInfo, UserInfo, BorrowInfo>(sql, (br, bi, brt, me, ui) =>
                    { br.BookInfo = bi; br.BorrowType = brt; br.MechanInfo = me; br.UserInfo = ui; return br; }
                        , parameters,
                        splitOn: "BookId,Bo_TypeId,MechanId,UserId").Single();
                }
            }
            catch (Exception ex)
            {

            }
            return null;


        }
    }
}
