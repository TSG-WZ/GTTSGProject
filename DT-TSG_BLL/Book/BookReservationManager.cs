using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using System;


namespace DTTSG_BLL
{
    public class BookReservationManager
    {
        BookReservationServer reservationServer = new BookReservationServer();
        NoticeServer noticeServer = new NoticeServer();

        BookServer bookServer = new BookServer();
        public Pager<ForwardInfo> GetReservationList(int pageIndex, int pageSize, int UserId = 0)
        {

            int dataCount = reservationServer.GetReservationList(UserId).Count;
            var InfoList = reservationServer.GetReservationList(UserId, true, pageIndex: pageIndex, pageSize: pageSize);

            Pager<ForwardInfo> pager = new Pager<ForwardInfo>(pageIndex, pageSize, dataCount, InfoList);
            return pager;

        }

        public ForwardInfo GetForwardModel(int Id)
        {
            var model = reservationServer.GetForwardInfoModelWithBookId(Id);
            if (model == null)
            {
                model = reservationServer.GetForwardInfoModelWithFowardId(Id);
            }
            return model;

        }

        /// <summary>
        /// 预约书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public int ResvervationBook(int userId, int bookId)
        {
            BookInfo book = bookServer.GetBookModel(bookId);
            book.B_StatuId = 3;

            ForwardInfo model = new ForwardInfo()
            {
                UserId = userId,
                BookId = bookId,
                F_StartTime = DateTime.Now,
                F_EndTime = DateTime.Now.AddHours(1)
            };
            int result = reservationServer.Insert(model);
            if (result == 1)
            {
                bookServer.Update(book);
                Notice notice = new Notice()
                {
                    NoticeTime = DateTime.Now,
                    N_TypeId = 3,
                    NoticeTitle = "预约成功提醒",
                    NoticeContent = "恭喜您成功预约" + book.BookName + "这本书！请在一小时之内取走，否则需要您重新预约",
                    UserId = userId,
                    LibId = 1001

                };

                noticeServer.Insert(notice);
                return 1;
            }
            else
            {
                return 0;
            }
        }



    }
}
