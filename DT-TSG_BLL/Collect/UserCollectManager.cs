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
        ImageServer imageServer = new ImageServer();
        /// <summary>
        /// 返回收藏列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserCollect> GetCollectList(int userId)
        {
            var InfoList = collectServer.GetCollectList(userId);
            foreach (var item in InfoList)
            {
               item.BookInfo.ImageInfo = imageServer.GetimageInfo(item.BookInfo.ImageId);
            }

            return InfoList;
        }

        public Pager<UserCollect> GetCollectPagerList(int userId,int pageIndex,int pageSize)
        {
            int dataCount = collectServer.GetCollectList(userId).Count;
            var InfoList = collectServer.GetCollectList(userId, true, pageIndex: pageIndex, pageSize: pageSize);
            foreach (var item in InfoList)
            {
                item.BookInfo.ImageInfo = imageServer.GetimageInfo(item.BookInfo.ImageId);
            }

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
        /// 收藏校验  去重
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns>true 不能在添加</returns>
        public bool CollectCheck(int userId,int bookId)
        {
            return collectServer.GetCollectModels(bookId:bookId,userId:userId).Count == 0;
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
