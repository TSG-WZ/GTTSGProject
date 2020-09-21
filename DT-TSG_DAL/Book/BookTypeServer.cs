using DTTSG_Model;
using System.Collections.Generic;

namespace DTTSG_DAL.Book
{
    public class BookTypeServer:BaseServer<BookType>
    {
        public List<BookType> GetBooKTypeList()
        {
            return GetList("select * from BookType");
        }
    }
}
