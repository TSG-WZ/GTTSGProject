using Dapper;
using DTTSG_Common;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_DAL.Book
{
    public class BookServer
    {
        public List<BookInfo> GetBookList()
        {
            List<BookInfo> list = null;
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    string sql = @"select * from BookInfo bi join BookType bt on
                                    bi.B_TypeId=bt.B_TypeId join BookStatu bs on
                                    bi.B_StatuId=bs.B_StatuId join MechanInfo me on
                                    bi.MechanId=me.MechanId join ImageInfo im on 
                                    bi.ImageId=im.ImageId where bi.B_StatuId<2";
                    list = connection.Query<BookInfo, BookType,BookStatu,MechanInfo,ImageInfo, BookInfo>
                        (sql,(bi,bt,bs,me,im)=> 
                        { bi.BookType = bt;bi.BookStatu = bs;bi.MechanInfo = me;bi.ImageInfo=im ;return bi; },
                        splitOn: "B_TypeId,B_StatuId,MechanId,ImageId").ToList();
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return list;
        }
    }
}
