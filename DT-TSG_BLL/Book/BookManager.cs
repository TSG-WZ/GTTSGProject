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
            return bookServer.GetBookList();
        }
    }
}
