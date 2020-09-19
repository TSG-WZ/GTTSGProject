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
        public Pager<BookInfo> GetBookList(BookInfo book)
        {
            DynamicParameters parameters = new DynamicParameters();
            string sql = @"select * from UserInfo a inner join UserType b 
            on a.TypeId = b.TypeId order by UserId asc offset(@pageIndex - 1) *
            @pageSize rows fetch next @pageSize rows only";

            List<BookInfo> bookInfoList = GetList(sql,parameters);

            Pager<BookInfo> bookInfoPager = new Pager<BookInfo>()
            {
                InfoList=bookInfoList,
                r


            };



            return ;
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
