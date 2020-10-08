﻿using Dapper;
using DTTSG_Common;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_DAL
{
    public class NoticeServer:BaseServer<Notice>
    {
        public List<Notice> GetNoticeList(int userId,bool isPager = false, int pageIndex = 0, int pageSize = 0,int isRead=-1)
        {
            string sql = @"select * from Notice nt join NoticeType ntt on nt.N_TypeId = ntt.N_TypeId";
            DynamicParameters parameters = new DynamicParameters();
            if (userId !=0 || isRead !=-1)
            {
                sql += " where ";
            }
            if (userId != 0)
            {
                parameters.Add("@UserId", userId);
                
                sql += " UserId = @UserId";
            }
            if (isRead!=-1)
            {
                parameters.Add("@isRead",isRead == 1);
                sql += " isRead = @isRead";
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
                    return connection.Query<Notice,NoticeType,Notice>(sql, (nt, ntt) =>
                    { nt.NoticeType= ntt; return nt; }
                        , parameters,
                        splitOn: "N_TypeId").ToList();
                };
                
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}