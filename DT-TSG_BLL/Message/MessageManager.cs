using DTTSG_Common;
using DTTSG_DAL.Message;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_BL
{
    public class MessageManager
    {
        MessageServer messageServer = new MessageServer();

        public Pager<MessageInfo> GetMessageList(int pageIndex=0,int pageSize=0,int isValid=-1)
        {
            int dataCount = messageServer.GetMessageInfoList(isValid:isValid).Count;
            List<MessageInfo> InfoList = messageServer.GetMessageInfoList(true,pageIndex, pageSize,isValid);

            Pager<MessageInfo> pager = new Pager<MessageInfo>(pageIndex, pageSize, dataCount, InfoList);
            return pager;
        }

        public int AddMessageInfo(MessageInfo messageInfo)
        {
            return messageServer.Insert(messageInfo);
        }

        public int DelMessage(MessageInfo messageInfo)
        {
            return messageServer.Delete(messageInfo);
        }

        public int UpdateMessage(MessageInfo messageInfo)
        {
            return messageServer.Update(messageInfo);
        }
    }
}
