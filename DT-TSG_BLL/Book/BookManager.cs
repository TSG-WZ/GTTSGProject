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

        public List<BookInfo> GetBookList()
        {
            MechanInfo m = new MechanInfo() { MechanName="bglb"};
            BookInfo bookInfo = new BookInfo()
            {
                MechanInfo = m
            };
            return bookServer.GetBookList(bookInfo);
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
