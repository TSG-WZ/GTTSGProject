using DTTSG_Common;
using DTTSG_DAL.Book;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_BLL.Book
{
    public class BookManager
    {
        BookServer bookServer = new BookServer();

        public Pager<BookInfo> GetBookList(int pagesize, int pageindex, BookInfo bookInfo)
        {
            bookInfo.MechanInfo = new MechanInfo() { MechanName = Config.GetHostName };   //机器信息

            //int dataCount = bookServer.GetBookCount(); //获取数据总数
            int dataCount = 30;
            List<BookInfo> list = bookServer.GetBookList(pagesize, pageindex, bookInfo);
            Pager<BookInfo> pager = new Pager<BookInfo>(pageindex, pagesize, dataCount, list);

            return pager;
        }



        public int InsertBookInfo(BookInfo bookInfo)
        {
            return bookServer.InsertBookInfo(bookInfo);
        }

        public int DelBookInfo(BookInfo bookInfo)
        {
            return bookServer.DelBookInfo(bookInfo);

        }

        public int UpdateBookInfo(BookInfo bookInfo)
        {
            return bookServer.UpdateBookInfo(bookInfo);

        }
    }
}
