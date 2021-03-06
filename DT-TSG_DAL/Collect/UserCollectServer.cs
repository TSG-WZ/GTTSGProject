﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DTTSG_Common;
using DTTSG_Model;
namespace DTTSG_DAL
{
    public class UserCollectServer:BaseServer<UserCollect>
    {
        /// <summary>
        /// 默认不排序
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="isPager"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<UserCollect> GetCollectList(int UserId, bool isPager = false, int pageIndex = 0, int pageSize = 0)
        {
            string sql = @"select * from UserCollect uc join BookInfo bi on
                            uc.BookId=bi.BookId ";
            DynamicParameters parameters = new DynamicParameters();

            if (UserId != 0)
            {
                parameters.Add("@UserId", UserId);
                sql += " where UserId = @UserId";
            }
            if (isPager)
            {
                parameters.Add("@pageIndex", pageIndex);
                parameters.Add("@pageSize", pageSize);
                //加排序
                sql += " order by CollectId asc offset(@pageIndex - 1) * @pageSize " +
                    "rows fetch next @pageSize rows only";
            }

            try
            {

                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<UserCollect, BookInfo, UserCollect>(sql, (uc, bi) =>
                    { uc.BookInfo = bi; return uc; }
                        , parameters,
                        splitOn: "BookId").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public List<UserCollect> GetCollectModels(int bookId,int userId)
        {
            string sql = @"select * from UserCollect where BookId=@BookId and userId = @userId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BookId",bookId);
            parameters.Add("@userId",userId);

            return GetList(sql,parameters);
        }
    }
}
