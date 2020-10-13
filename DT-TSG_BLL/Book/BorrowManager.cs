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
            List<BorrowInfo> InfoList = borrowServer.GetBorrowList(userInfo.UserId,true ,pageIndex, pageSize );

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

        //借书
        public int BookBrrow(int bookId, int userId)
        {
            CheckDeferBook(userId);
            var bookInfo = bookServer.GetBookModel(bookId);
         
            var borrowInfo = new BorrowInfo()
            {
                UserId = userId,
                BookId = bookId,
                Bo_TypeId = 1,
                B_MechanId = bookInfo.MechanId,
                B_StartTime = DateTime.Now,
                B_EndTime = DateTime.Now.AddMonths(1),
                B_ReturnTime = Convert.ToDateTime("1900/1/1 00:00:00"),

            };
  

            if (bookInfo != null && bookInfo.B_StatuId != 2)
            {
               
                bookInfo.B_StatuId = 2;

                int result = bookServer.Update(bookInfo);
                int a = borrowServer.Insert(borrowInfo);
                Notice notice = new Notice()
                {
                    NoticeTime = DateTime.Now,
                    N_TypeId = 2,
                    NoticeTitle = "借阅成功提醒",
                    NoticeContent = "恭喜您成功借阅《" + bookInfo.BookName + "》，请在一个月之内归还！",
                    UserId = userId,
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

        /// <summary>
        /// 返回0 已延期
        /// </summary>
        /// <param name="borrowId"></param>
        /// <param name="bookId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

            if (borrowInfo != null && DateTime.Compare(borrowInfo.B_EndTime, DateTime.Now) > 0)// 借阅状态 借阅时间内允许还书
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

        public int DeferOperation(int Bo_Id)
        {
            var BoModel = borrowServer.GetBorrowInfoModel(Bo_Id);
            
            Notice notice = new Notice()
            {
                UserId = BoModel.UserId,
                NoticeTitle = "书籍借阅到期提醒",
                NoticeContent = "您借阅的书籍：《" + BoModel.BookInfo.BookName + "》，由于过期未归还，请联系管理员归还图书！",
                NoticeTime = BoModel.B_EndTime,
                LibId = 1001,
                N_TypeId = 1
            };
            if (noticeServer.GetNotice(BoModel.B_EndTime) == null)
            {
                noticeServer.Insert(notice);
            }

            BoModel.Bo_TypeId = 2;
            return  borrowServer.Update(BoModel);
        }

        public void CheckDeferBook(int userId)
        {
            var list = borrowServer.GetBorrowList(userId:userId);
            foreach (var item in list)
            {
                if (DateTime.Compare(item.B_EndTime, DateTime.Now) < 0 && item.Bo_TypeId == 1)
                {
                    DeferOperation(item.BorrowId);
                }
            }
        }
    }
}
