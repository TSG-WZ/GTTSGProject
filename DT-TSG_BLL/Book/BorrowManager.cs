using DTTSG_Common;
using DTTSG_DAL.Book;
using DTTSG_Model;
using System.Collections.Generic;

namespace DTTSG_BLL.Book
{
    public class BorrowManager
    {
        BorrowServer borrowServer = new BorrowServer();

        public Pager<BorrowInfo> GetBorrowInfoList(int pageIndex, int pageSize, UserInfo userInfo)
        {
            int dataCount = borrowServer.GetBorrowListLength(userInfo.UserId);
            List<BorrowInfo> InfoList = borrowServer.GetBorrowList(pageIndex, pageSize, userInfo);

            Pager<BorrowInfo> pager = new Pager<BorrowInfo>(pageIndex, pageSize, dataCount, InfoList);
            return pager;
        }

        public BorrowInfo GetBorrowInfoModel(int borrowId)
        {

            if (borrowId == 0)
            {
                return null;
            }

            return borrowServer.GetBorrowInfoModel(borrowId);
        }
    }
}
