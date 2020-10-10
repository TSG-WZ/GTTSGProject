using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using System;
using System.Collections.Generic;

namespace DTTSG_BLL
{
    public class BorrowManager
    {
        BorrowServer borrowServer = new BorrowServer();
        BookServer bookServer = new BookServer();
        NoticeServer noticeServer = new NoticeServer();
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

        public int BookBrrow(int bookId, int useId)
        {
            var bookInfo = bookServer.GetBookModel(bookId);
            var borrowInfo = new BorrowInfo()
            {
                UserId = useId,
                BookId = bookId,
                Bo_TypeId = 1,
                B_MechanId = bookInfo.MechanId,
                B_StartTime = DateTime.Now,
                B_EndTime = DateTime.Now.AddMonths(1),
                B_ReturnTime = Convert.ToDateTime("1900/1/1 00:00:00"),

            };

            bookInfo.B_StatuId = 2;
            if (bookInfo != null)
            {

                int result = bookServer.Update(bookInfo);
                int a = borrowServer.Insert(borrowInfo);
                Notice notice = new Notice()
                {
                    NoticeTime = DateTime.Now,
                    N_TypeId = 2,
                    NoticeTitle = "借阅成功提醒",
                    NoticeContent = "恭喜您成功借阅《" + bookInfo.BookName + "》，请在一个月之内归还！",
                    UserId = useId,
                    LibId = 1001
                };

                noticeServer.Insert(notice);
                return a == result ? 1 : 0;


            }
            else
            {
                return -1;
            }

        }

        public int BookReturn(int borrowId, int bookId = 0, int userId = 0)
        {
            BorrowInfo borrowInfo;
            if (bookId == 0)
            {
                borrowInfo = borrowServer.GetBorrowInfoModel(borrowId);
            }
            else
            {
                borrowInfo = borrowServer.GetBorrowInfoModel(0, bookId);
            }

            if (borrowInfo != null)
            {
                borrowInfo.Bo_TypeId = 3;
                borrowInfo.B_ReturnTime = DateTime.Now;
                var a = borrowServer.Update(borrowInfo);
                var bookInfo = borrowInfo.BookInfo;
                bookInfo.B_StatuId = 1;
                var b = bookServer.Update(bookInfo);

                Notice notice = new Notice()
                {
                    NoticeTime = DateTime.Now,
                    N_TypeId = 2,
                    NoticeTitle = "还书成功提醒",
                    NoticeContent = "恭喜您成功还书《" + bookInfo.BookName + "》感谢使用本系统！",
                    UserId = userId,
                    LibId = 1001
                };
                noticeServer.Insert(notice);
                return a == b ? 1 : 0;
            }

            return 0;
        }

        
    }
}
