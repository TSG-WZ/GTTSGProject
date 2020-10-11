using DTTSG_DAL;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_BLL
{
    public class RankManager
    {
        BorrowServer borrowServer = new BorrowServer();
        RankList rank = new RankList();
        // 用户借阅总数
        public int BrrowBookCount(int userId)
        {
            return borrowServer.GetBorrowList(userId).Count();
           
        }

        // 借阅中
        public int BrrowBookIng(int userId)
        {
            return borrowServer.GetBorrowList(userId).Where(b => b.Bo_TypeId==1).Count();
        }

        // 延期中
        public int BrrowBookEd(int userId)
        {
            return borrowServer.GetBorrowList(userId).Where(b => b.Bo_TypeId == 2).Count();
        }

        public List<BookInfo> GetMaxBorrowBook(int count)
        {
            if (count == 0)
            {
                return null;
            }
            return rank.GetMaxBorrowBook(count);
        }

        public List<UserInfo> GetMaxBorrowUser(int count)
        {
            if (count == 0)
            {
                return null;
            }
            return rank.GetMaxBorrowUser(count);
        }


        public int GetBorrowUserCount(int userId)
        {
            return rank.GetAllBorrowInfo().Where(b=>b.UserId==userId).Count();
        }

        public int GetBorrowBookCount(int bookId)
        {
            return rank.GetAllBorrowInfo().Where(b => b.BookId == bookId).Count();
        }

    }
}
