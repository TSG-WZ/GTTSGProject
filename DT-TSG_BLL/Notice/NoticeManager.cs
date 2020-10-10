using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using System.Collections.Generic;
namespace DTTSG_BLL
{
    public class NoticeManager
    {
        NoticeServer noticeServer = new NoticeServer();
        public List<Notice> GetNoticeList(int userId,int isRead=-1)
        {
            // 获取所有的消息
           return noticeServer.GetNoticeList(userId,isRead:isRead);

        }

        public Pager<Notice> GetNoticePagerList(int pageIndex, int pageSize, int userId, int isRead = -1)
        {
  
            int dataCount = noticeServer.GetNoticeList(userId).Count;
            var InfoList = noticeServer.GetNoticeList(userId, true, pageIndex: pageIndex, pageSize: pageSize);

            Pager<Notice> pager = new Pager<Notice>(pageIndex, pageSize, dataCount, InfoList);
            return pager;

        }

        public int AddNotice(Notice notice)
        {
            notice.IsRead = false;
            return noticeServer.Insert(notice);
        }

        public int ReadNotice(int noticeId)
        {
            var notice = noticeServer.GetNoticeModel(noticeId);

            notice.IsRead = true;

            return noticeServer.Update(notice);
        }
      

    }
}
