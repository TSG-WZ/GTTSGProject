using DTTSG_Common;
using DTTSG_DAL.Book;
using DTTSG_Model;
using System;
using System.Collections.Generic;

namespace DTTSG_BLL.Book
{
    public class BookManager
    {
        BookServer bookServer = new BookServer();
        BorrowServer borrowServer = new BorrowServer();

        /// <summary>
        ///  分页
        /// </summary>
        /// <param name="b_TypeId">传入book_TypeId</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public Pager<BookInfo> GetBookList(int b_TypeId, int pageIndex, int pageSize,bool isWeiChat=false)
        {
            int dataCount = bookServer.GetBookListLength();
            List<BookInfo> InfoList = bookServer.GetBookList(pageIndex, pageSize, b_TypeId,isWeiChat);

            Pager<BookInfo> pager = new Pager<BookInfo>(pageIndex, pageSize, dataCount, InfoList);
            return pager;
        }


        /// <summary>
        /// 通过Id 查询实体
        /// </summary>
        /// <returns></returns>
        public BookInfo GetBookModel(int bookId)
        {

            if (bookId == 0)
            {
                return null;
            }

            return bookServer.GetBookModel(bookId);
        }

        public int BookBrrow(int bookId, UserInfo userInfo)
        {
            var bookInfo = GetBookModel(bookId);
            var borrowInfo = new BorrowInfo()
            {
                UserId = userInfo.UserId,
                BookId = bookId,
                Bo_TypeId = 1,
                B_MechanId = bookInfo.MechanId,
                B_StartTime = DateTime.Now,
                B_EndTime = DateTime.Now.AddMonths(1),
                B_ReturnTime = Convert.ToDateTime("1900/1/1 00:00:00"),
                //BookInfo  = new BookInfo(),
                //UserInfo = new UserInfo(),
                //MechanInfo = new MechanInfo(),
                //BorrowType = new BorrowType()

            };
            bookInfo.B_StatuId = 2;
            if (bookInfo != null)
            {
                int result = bookServer.Update(bookInfo);
                int a = borrowServer.Insert(borrowInfo);

                return a == result ? 1 : 0;


            }
            else
            {
                return -1;
            }

        }

        public int BookReturn(int borrowId, int bookId=0)
        {
            BorrowInfo borrowInfo;
            if (bookId == 0)
            {
                borrowInfo = borrowServer.GetBorrowInfoModel(borrowId);
            }
            else
            {
               borrowInfo = borrowServer.GetBorrowInfoModel(0,bookId);
            }

            if (borrowInfo != null)
            {
                borrowInfo.Bo_TypeId = 3;
                borrowInfo.B_ReturnTime = DateTime.Now;
                var a = borrowServer.Update(borrowInfo);
                var bookInfo = borrowInfo.BookInfo;
                bookInfo.B_StatuId = 1;
                var b = bookServer.Update(bookInfo);
                return a == b ? 1 : 0;
            }

            return 0;
        }

        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public int InsertBookInfo(BookInfo bookInfo)
        {
            return bookServer.Insert(bookInfo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public int DelBookInfo(BookInfo bookInfo)
        {
            return bookServer.Delete(bookInfo);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public int UpdateBookInfo(BookInfo bookInfo)
        {
            return bookServer.Update(bookInfo);
        }


    }
}
