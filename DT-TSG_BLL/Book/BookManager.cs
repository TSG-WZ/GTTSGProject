using DTTSG_Common;
using DTTSG_DAL.Book;
using DTTSG_Model;
using System.Collections.Generic;

namespace DTTSG_BLL.Book
{
    public class BookManager
    {
        BookServer bookServer = new BookServer();

        /// <summary>
        ///  分页
        /// </summary>
        /// <param name="b_TypeId">传入book_TypeId</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public Pager<BookInfo> GetBookList(int b_TypeId,int pageIndex, int pageSize )
        {
            int dataCount = bookServer.GetBookListLength();
            List<BookInfo> InfoList = bookServer.GetBookList(pageIndex, pageSize, b_TypeId);

            Pager<BookInfo> pager = new Pager<BookInfo>(pageIndex, pageSize, dataCount, InfoList);
            return pager;
        }


        /// <summary>
        /// 通过Id 查询实体
        /// </summary>
        /// <returns></returns>
        public BookInfo GetBookModel(int bookId)
        {
          
            if (bookId ==0)
            {
                return null;
            }
     
            return bookServer.GetBookModel(bookId);
        }




        /// <summary>
        /// 插入新数据
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public int InsertBookInfo(BookInfo bookInfo)
        {
            return bookServer.InsertBookInfo(bookInfo);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public int DelBookInfo(BookInfo bookInfo)
        {
            return bookServer.DelBookInfo(bookInfo);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public int UpdateBookInfo(BookInfo bookInfo)
        {
            return bookServer.UpdateBookInfo(bookInfo);
        }
    }
}
