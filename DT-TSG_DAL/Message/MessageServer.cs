using Dapper;
using DTTSG_Common;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace DTTSG_DAL.Message
{
    public class MessageServer : BaseServer<MessageInfo>
    {
        public List<MessageInfo> GetMessageInfoList(bool isPager = false, int pageIndex = 0, int pageSize = 0, int isValid = -1)
        {
            string sql = @"select * from MessageInfo mei join MessageType met on mei.M_TypeId = met.M_TypeId";
            DynamicParameters parameters = new DynamicParameters();

            if (isValid!=-1)
            {
                parameters.Add("@isValid",isValid==1);
                sql += " where isValid = @isValid";
            }

            if (isPager)
            {
                parameters.Add("@pageIndex", pageIndex);
                parameters.Add("@pageSize", pageSize);
                //加排序
                sql += " order by NoticeId asc offset(@pageIndex - 1) * @pageSize " +
                    "rows fetch next @pageSize rows only";
            }
            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<MessageInfo, MessageType, MessageInfo>(sql, (mei, met) =>
                    { mei.MessageType = met; return mei; }
                        , parameters,
                        splitOn: "M_TypeId").ToList();
                };

            }
            catch (Exception ex)
            {

            }
            return null;

        }
    }
}
