using System.Collections.Generic;

namespace DTTSG_Common
{
    public class Pager<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 分页总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> InfoList { get; set; }

        /// <summary>
        /// 无参数构造方法
        /// </summary>
        public Pager()
        {

        }

        /// <summary>
        /// 构造方法赋值
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="dataCount">数据总条数</param>
        /// <param name="infoList">数据列表</param>
        public Pager(int pageIndex, int pageSize, int dataCount, List<T> infoList)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.DataCount = dataCount;
            this.PageCount = dataCount / PageSize;
            if (dataCount % pageSize != 0)
            {
                this.PageCount++;
            }
            this.InfoList = infoList;
        }
       




    }
}

