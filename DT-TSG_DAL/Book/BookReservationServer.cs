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

namespace DTTSG_DAL
{
    public class BookReservationServer:BaseServer<ForwardInfo>
    {
        public List<ForwardInfo> GetReservationList(int UserId,int FoTypeId=0 ,bool isPager =false,int pageIndex=0,int pageSize=0)
        {

            string sql = @"select * from ForwardInfo fd join BookInfo bi on
                            fd.BookId=bi.BookId join FowardType ft on fd.Fo_TypeId = ft.Fo_TypeId where 1=1 ";
            DynamicParameters parameters = new DynamicParameters();
         
            if (UserId != 0)
            {
                parameters.Add("@UserId", UserId);
                sql += " and UserId = @UserId";
            }
            if (FoTypeId != 0)
            {
                parameters.Add("@Fo_TypeId",FoTypeId);
                sql += " and fd.Fo_TypeId = @Fo_TypeId";
            }
          
            if (isPager)
            {
                parameters.Add("@pageIndex", pageIndex);
                parameters.Add("@pageSize", pageSize);
                //加排序
                sql += " order by F_EndTime desc offset(@pageIndex - 1) * @pageSize " +
                    "rows fetch next @pageSize rows only";
            }

          
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<ForwardInfo, BookInfo ,FowardType,ForwardInfo >(sql, (fd, bi,ft) =>
                    { fd.BookInfo = bi; fd.FowardType = ft; return fd; }
                        , parameters,
                        splitOn: "BookId,Fo_TypeId").ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public ForwardInfo GetForwardInfoModelWithBookId(int BookId)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sql = @"select * from ForwardInfo fd join BookInfo bi on fd.BookId = bi.BookId where BookId = @BooKId";
            parameters.Add("@BooKId", BookId);

            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<ForwardInfo, BookInfo, ForwardInfo>(sql, (fd, bi) =>
                    { fd.BookInfo = bi; return fd; }
                        , parameters,
                        splitOn: "BookId").Single();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public ForwardInfo GetForwardInfoModelWithFowardId(int FdId)
        {
            string sql = @"select * from ForwardInfo fd join BookInfo bi on fd.BookId = bi.BookId where fd.F_Id = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", FdId);

            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<ForwardInfo, BookInfo, ForwardInfo>(sql, (fd, bi) =>
                    { fd.BookInfo = bi; return fd; }
                        , parameters,
                        splitOn: "BookId").Single();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
