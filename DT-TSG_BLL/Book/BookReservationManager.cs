using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTTSG_BLL
{
    public class BookReservationManager
    {
        BookReservationServer reservationServer = new BookReservationServer();
        NoticeServer noticeServer = new NoticeServer();
        BookServer bookServer = new BookServer();
        ImageServer imageServer = new ImageServer();

        public List<ForwardInfo> GetForwardInfoList(int userId = 0)
        {
            return reservationServer.GetReservationList(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="UserId"></param>
        /// <param name="FoTypeId">预约状态，默认0 所有  预约中 1  已取走 2  已过期 3 </param>
        /// <returns></returns>
        public Pager<ForwardInfo> GetReservationPagerList(int pageIndex, int pageSize, int UserId = 0, int FoTypeId = 0)
        {

            int dataCount = reservationServer.GetReservationList(UserId,FoTypeId: FoTypeId).Count;

            List<ForwardInfo> InfoList = reservationServer.GetReservationList(UserId,FoTypeId, true, pageIndex: pageIndex, pageSize: pageSize);
           
            foreach (var item in InfoList)
            {
                item.BookInfo.ImageInfo = imageServer.GetimageInfo(item.BookInfo.ImageId);
            }

            Pager<ForwardInfo> pager = new Pager<ForwardInfo>(pageIndex, pageSize, dataCount, InfoList);
            return pager;
        }

        /// <summary>
        ///  预约时间内取书
        /// </summary>
        /// <param name="foId"></param>
        /// <returns></returns>
        public int FetchBook(int foId)
        {
            var model = reservationServer.GetForwardInfoModelWithFowardId(foId);
            if (model.Fo_TypeId == 1)
            {

                model.Fo_TypeId = 2;
                return reservationServer.Update(model);
            }
            else
            {
                return -1;
            }
        }

        public ForwardInfo GetForwardModel(int Id)
        {
            var model = reservationServer.GetForwardInfoModelWithBookId(Id);
            if (model == null)
            {
                model = reservationServer.GetForwardInfoModelWithFowardId(Id);
            }
            model.BookInfo.ImageInfo = imageServer.GetimageInfo(model.BookInfo.ImageId);
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

            if (book.B_StatuId == 3) return -1;     //已被预约

            book.B_StatuId = 3;

            ForwardInfo model = new ForwardInfo()
            {
                UserId = userId,
                BookId = bookId,
                F_StartTime = DateTime.Now,
                F_EndTime = DateTime.Now.AddHours(1),
                Fo_TypeId = 1
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
                    NoticeContent = "恭喜您成功预约《" + book.BookName + "》这本书！请在一小时之内取走，否则需要您重新预约",
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
        // 取消预约
        public int CancelResvervationBook(int F_Id)
        {
            var fModel = reservationServer.GetForwardInfoModelWithFowardId(F_Id);
            fModel.Fo_TypeId = 3;
            fModel.BookInfo.B_StatuId = 1;
            Notice notice = new Notice()
            {
                UserId = fModel.UserId,
                NoticeTitle = "预约过期提醒",
                NoticeContent = "您预约的书籍：《" + fModel.BookInfo.BookName + "》，由于过期未取，现在系统已经自动取消！",
                NoticeTime = fModel.F_EndTime,
                LibId = 1001,
                N_TypeId = 3
            };
            if (noticeServer.GetNotice(fModel.F_EndTime) == null)
            {
                noticeServer.Insert(notice);
            }
            bookServer.Update(fModel.BookInfo);

            return reservationServer.Update(fModel);
        }

        // 判断是否预约过期
        public void ResvervationCheck()
        {
            var list = GetForwardInfoList();
            foreach (var item in list)
            {
                if (DateTime.Compare(item.F_EndTime, DateTime.Now) < 0 && item.Fo_TypeId == 1)
                {
                    CancelResvervationBook(item.F_Id);
                }
            }
        }

        public int UpdateResvervation(ForwardInfo forwardInfo)
        {
            return reservationServer.Update(forwardInfo);
        }

    }
}
