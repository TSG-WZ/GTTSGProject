using Dapper;
using DTTSG_Common;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DTTSG_DAL.Book
{
    public class BookServer : BaseServer<BookInfo>
    {
        /// <summary>
        /// 分页联查 按照Id排序
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="bookType">分类类型</param>
        /// <returns>BookInfo列表</returns>
        public List<BookInfo> GetBookList(int pageIndex,int pageSize,int b_TypeId = 0,bool isWeiChat=false)
        {
            //sql参数
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pageIndex",pageIndex);
            parameters.Add("@pageSize", pageSize);
           

            string sql = @"select * from BookInfo bi join BookType bt on
                            bi.B_TypeId=bt.B_TypeId join BookStatu bs on
                            bi.B_StatuId=bs.B_StatuId join MechanInfo me on
                            bi.MechanId=me.MechanId join ImageInfo im on 
                            bi.ImageId=im.ImageId where bi.B_StatuId<2 ";
            if (!isWeiChat)
            {
                parameters.Add("@MechanName", Config.GetHostName);
                sql += " and  me.MechanName = @MechanName ";
            }
            // 加分类和分类参数
            if (b_TypeId != 0)
            {
                parameters.Add("@TypeId", b_TypeId);
                sql += " and bt.B_TypeId=@TypeId";
            }
            //加排序
            sql += " order by BookId asc offset(@pageIndex - 1) * @pageSize " +
                "rows fetch next @pageSize rows only";
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<BookInfo, BookType, BookStatu, MechanInfo, ImageInfo, BookInfo>(sql, (bi, bt, bs, me, im) =>
                        { bi.BookType = bt; bi.BookStatu = bs; bi.MechanInfo = me; bi.ImageInfo = im; return bi; }
                        , parameters,
                        splitOn: "B_TypeId,B_StatuId,MechanId,ImageId").ToList();
        
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public int GetBookListLength()
        {
            DynamicParameters parma = new DynamicParameters();
            parma.Add("@MechanName", Config.GetHostName);
            return GetList(@"select * from BookInfo bi join BookStatu bs on
                                    bi.B_StatuId = bs.B_StatuId join MechanInfo me on
                                    bi.MechanId = me.MechanId where bi.B_StatuId < 2 and me.MechanName = @MechanName"
                                    , parma).Count;

        }

        /// <summary>
        /// 查询得到 Model实体
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public BookInfo GetBookModel(int bookId)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {

                    string sql = @"select * from BookInfo bi join BookType bt on
                                    bi.B_TypeId=bt.B_TypeId join BookStatu bs on
                                    bi.B_StatuId=bs.B_StatuId join MechanInfo me on
                                    bi.MechanId=me.MechanId join ImageInfo im on 
                                    bi.ImageId=im.ImageId where bi.B_StatuId<2 
                                    and me.MechanName=@MechanName and bi.BookId=@BookId";

                    return connection.Query<BookInfo, BookType, BookStatu, MechanInfo, ImageInfo, BookInfo>
                        (sql, (bi, bt, bs, me, im) =>
                        { bi.BookType = bt; bi.BookStatu = bs; bi.MechanInfo = me; bi.ImageInfo = im; return bi; }
                        , new { MechanName=Config.GetHostName,BookId=bookId},
                        splitOn: "B_TypeId,B_StatuId,MechanId,ImageId").First();
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return null;
        }
    }
}
