using DTTSG_Common;
using DTTSG_DAL;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_BLL
{
    public class UserCollectManager
    {
        UserCollectServer collectServer = new UserCollectServer();

        /// <summary>
        /// 返回收藏列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserCollect> GetCollectList(int userId)
        {
            return collectServer.GetCollectList(userId);
        }

        public Pager<UserCollect> GetCollectPagerList(int userId,int pageIndex,int pageSize)
        {
            int dataCount = collectServer.GetCollectList(userId).Count;
            var InfoList = collectServer.GetCollectList(userId, true, pageIndex: pageIndex, pageSize: pageSize);

            Pager<UserCollect> pager = new Pager<UserCollect>(pageIndex, pageSize, dataCount, InfoList);
            return pager;
          
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public int AddCollect(int userId,int bookId)
        {
            UserCollect userCollect = new UserCollect()
            {
                BookId = bookId,
                UserId = userId,
            };

            return collectServer.Insert(userCollect);
        }

        /// <summary>
        /// 取消收藏 ，数据库彻底删除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public int CancelCollect(int userId,int bookId)
        {
            UserCollect userCollect = new UserCollect()
            {
                BookId = bookId,
                UserId = userId,
            };

            return collectServer.Delete(userCollect);
        }
    }
}
