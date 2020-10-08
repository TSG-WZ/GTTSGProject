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

        public int AddNotice(Notice notice)
        {
            notice.IsRead = false;
            return noticeServer.Insert(notice);
        }

        public int ReadNotice(Notice notice)
        {
            notice.IsRead = true;
            return noticeServer.Update(notice);
        }
      

    }
}
