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
        public List<BookInfo> GetBookList(int pageSize,int pageIndex,BookInfo book)
        {
            List<BookInfo> list = new List<BookInfo>();
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                { 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@pageSize", pageSize);
                parameters.Add("@pageIndex", pageIndex);
                string sql = @"select * from BookInfo bi join BookType bt on
                                    bi.B_TypeId=bt.B_TypeId join BookStatu bs on
                                    bi.B_StatuId=bs.B_StatuId join MechanInfo me on
                                    bi.MechanId=me.MechanId join ImageInfo im on 
                                    bi.ImageId=im.ImageId where bi.B_StatuId<2 and
                                    me.MechanName='bglb' order by BookId asc offset(@pageIndex - 1) *
                                @pageSize rows fetch next @pageSize rows only";
                list = connection.Query<BookInfo, BookType, BookStatu, MechanInfo, ImageInfo, BookInfo>
                        (sql, (bi, bt, bs, me, im) =>
                        { bi.BookType = bt; bi.BookStatu = bs; bi.MechanInfo = me; bi.ImageInfo = im; return bi; }
                        , parameters,
                        splitOn: "B_TypeId,B_StatuId,MechanId,ImageId").ToList();
                }
            }
            catch (Exception ex)
            {
                //报错堆栈
            }
            return list;
        }
        public BookInfo GetBookModel(BookInfo book)
        {
            string sql = "";
            return GetModel("");

        }

        public int InsertBookInfo(BookInfo bookInfo)
        {
            //string sql = @"INSERT INTO [dbo].[BookInfo] ([B_StatuId],[B_TypeId],[MechanId],[ImageId],[BookCode],[BookName]," +
            //     "[BookPrice],[BookPublic],[BookAuthor],[BookPubtime],[IsValid]) " +
            //     "VALUES(" +
            //     "@B_StatuId," +
            //     "@B_TypeId," +
            //     "@MechanId," +
            //     "@ImageId," +
            //     "@BookCode," +
            //     "@BookName," +
            //     "@BookPrice," +
            //     "@BookPublic," +
            //     "@BookAuthor," +
            //     "@BookPubtime," +
            //     "@IsValid)";
            // DynamicParameters parmters = new DynamicParameters();
            // parmters.Add("@B_StatuId", bookInfo.B_StatuId);
            // parmters.Add("@B_TypeId", bookInfo.B_TypeId);
            // parmters.Add("@MechanId", bookInfo.MechanId);
            // parmters.Add("@ImageId", bookInfo.ImageId);
            // parmters.Add("@BookCode", bookInfo.BookCode);
            // parmters.Add("@BookName", bookInfo.BookName);
            // parmters.Add("@BookPrice", bookInfo.BookPrice);
            // parmters.Add("@BookPublic", bookInfo.BookPublic);
            // parmters.Add("@BookAuthor", bookInfo.BookAuthor);
            // parmters.Add("@BookPubtime", bookInfo.BookPubtime);
            // parmters.Add("@IsValid", bookInfo.IsValid);
            //return  Add(bookInfo);
            var sqlobj = InsertAssembleSql(bookInfo);
            string sql = null;
            object parmters = null;
            foreach (var item in sqlobj)
            {
                if ("sql" == item.Key.ToString())
                {
                    sql = item.Value.ToString();
                }
                else
                {
                    parmters = item.Value;
                }

            }

            if (!string.IsNullOrEmpty(sql))
            {
                return Execute(sql, parmters);
            }
            return 0;


        }


        public int DelBookInfo(BookInfo bookInfo)
        {
            var sqlobj = DelAssembleSql(bookInfo);
            string sql = null;
            object parmters = null;
            foreach (var item in sqlobj)
            {
                if ("sql" == item.Key.ToString())
                {
                    sql = item.Value.ToString();
                }
                else
                {
                    parmters = item.Value;
                }

            }

            //string sql = @"DELETE FROM [dbo].[BookInfo] WHERE BookId=@BookId";
            //DynamicParameters parmters = new DynamicParameters();
            //parmters.Add("@BookId", bookInfo.BookId);
            if (!string.IsNullOrEmpty(sql))
            {
                return Execute(sql, parmters);
            }
            return 0;
        }


        public int UpdateBookInfo(BookInfo bookInfo)
        {
            //string sql = @"UPDATE [dbo].[BookInfo]"+
            //"SET [B_StatuId] = @B_StatuId,"+
            //"[B_TypeId] = @B_TypeId,"+
            //"[MechanId] = @MechanId,"+
            //"[ImageId] = @ImageId,"+
            //"[BookCode] = @BookCode,"+
            //"[BookName] = @BookName,"+
            //"[BookPrice] = @BookPrice,"+
            //"[BookPublic] = @BookPublic,"+
            //"[BookAuthor] = @BookAuthor,"+
            //"[BookPubtime] = @BookPubtime,"+
            //"[IsValid] = @IsValid "+
            //"WHERE BookId = @BookId";
            //DynamicParameters parmters = new DynamicParameters();
            //parmters.Add("@B_StatuId", bookInfo.B_StatuId);
            //parmters.Add("@B_TypeId", bookInfo.B_TypeId);
            //parmters.Add("@MechanId", bookInfo.MechanId);
            //parmters.Add("@ImageId", bookInfo.ImageId);
            //parmters.Add("@BookCode", bookInfo.BookCode);
            //parmters.Add("@BookName", bookInfo.BookName);
            //parmters.Add("@BookPrice", bookInfo.BookPrice);
            //parmters.Add("@BookPublic", bookInfo.BookPublic);
            //parmters.Add("@BookAuthor", bookInfo.BookAuthor);
            //parmters.Add("@BookPubtime", bookInfo.BookPubtime);
            //parmters.Add("@IsValid", bookInfo.IsValid);
            //parmters.Add("@BookId", bookInfo.BookId);
            //return Execute(sql, parmters);


            var sqlobj = UpdateAssembleSql(bookInfo);
            string sql = null;
            object parmters = null;
            foreach (var item in sqlobj)
            {
                if ("sql" == item.Key.ToString())
                {
                    sql = item.Value.ToString();
                }
                else
                {
                    parmters = item.Value;
                }

            }
            if (!string.IsNullOrEmpty(sql))
            {
                return Execute(sql, parmters);
            }
            return 0;
        }
    }
}
