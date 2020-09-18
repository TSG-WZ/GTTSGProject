﻿using Dapper;
using DTTSG_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DTTSG_DAL
{
    public class BaseServer<T> where T : class, new()
    {
        
        protected int Execute(string sql, object s = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    int result = connection.Execute(sql,s);
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return 0;
        }

        public virtual Dictionary<string, object> InsertAssembleSql(T model)
        {
            
            Dictionary<string, object> sqlobj = new Dictionary<string, object>();
            if (model != null)
            {
                string sql = @"insert into ";
                string TableName = typeof(T).Name;// 表名称
                Type type = model.GetType();
                DynamicParameters parameters = new DynamicParameters();
                sql += TableName;
                string sqlName = " (";
                string sqlValues = " values(";
                PropertyInfo[] propertyInfos = type.GetProperties();
                var KEY = propertyInfos.Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault();
                foreach (PropertyInfo item in propertyInfos)
                {
                    string PropertyName = item.Name.ToString();
                    var PropertyValue = item.GetValue(model, null) ?? "";
                    if (!PropertyValue.ToString().StartsWith("DTTSG") && PropertyName != KEY.Name.ToString())// 排除外联表以及主键
                    {
                        sqlName += PropertyName;
                        sqlValues += "@" + PropertyName;
                        parameters.Add("@" + PropertyName, PropertyValue);
                        if (!propertyInfos.LastOrDefault().Equals(item))
                        {
                            sqlName += ",";
                            sqlValues += ",";
                        }
                        else
                        {
                            sqlName += ")";
                            sqlValues += ")";
                        }

                    }

                }           
                sql += (sqlName + sqlValues);
                sqlobj.Add("sql", sql);
                sqlobj.Add("parameters", parameters);

            }
            else
            {
                sqlobj.Add("sql", string.Empty);
                sqlobj.Add("parameters", null);
            }
            return sqlobj;

        }

        public virtual Dictionary<string,object> UpdateAssembleSql(T model)
        {
            Dictionary<string, object> sqlobj = new Dictionary<string, object>();
            if (model != null)
            {
                string sql = @"update " ;
                string TableName = typeof(T).Name;// 表名称
                Type type = model.GetType();
                DynamicParameters parameters = new DynamicParameters();
                sql += TableName;
                string sqlSet = " set ";
                
                PropertyInfo[] propertyInfos = type.GetProperties();
                var KEY = propertyInfos.Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault();

                string sqlWhere = "where " + KEY.Name + "=@" + KEY.Name;
                parameters.Add("@" + KEY.Name, KEY.GetValue(model));// 添加主键作为where选择条件
                foreach (PropertyInfo item in propertyInfos)
                {
                    string PropertyName = item.Name.ToString();
                    var PropertyValue = item.GetValue(model, null) ?? "";
                    if (!PropertyValue.ToString().StartsWith("DTTSG") && PropertyName != KEY.Name.ToString())// 排除外联表以及主键
                    {                    
                        sqlSet+= PropertyName + "=@"+PropertyName;
                        parameters.Add("@" + PropertyName, PropertyValue);
                        if (!propertyInfos.LastOrDefault().Equals(item))
                        {
                            sqlSet += ", ";
                            
                        }
                        else
                        {
                            sqlSet += " ";
                        }

                    }

                }
                sql += (sqlSet + sqlWhere);
                sqlobj.Add("sql", sql);
                sqlobj.Add("parameters", parameters);

            }
            else
            {
                sqlobj.Add("sql", string.Empty);
                sqlobj.Add("parameters", null);
            }
            return sqlobj;

        }

        public virtual Dictionary<string, object> DelAssembleSql(T model)
        {
            Dictionary<string, object> sqlobj = new Dictionary<string, object>();
            if (model != null)
            {
                string sql = @"delete from ";
                string TableName = typeof(T).Name;// 表名称
                Type type = model.GetType();
                DynamicParameters parameters = new DynamicParameters();
                sql += TableName;
               

                PropertyInfo[] propertyInfos = type.GetProperties();
                var KEY = propertyInfos.Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).FirstOrDefault();

                string sqlWhere = " where " + KEY.Name + "=@" + KEY.Name;
                parameters.Add("@" + KEY.Name, KEY.GetValue(model));// 添加主键作为where选择条件
                
                sql += sqlWhere;
                sqlobj.Add("sql", sql);
                sqlobj.Add("parameters", parameters);

            }
            else
            {
                sqlobj.Add("sql", string.Empty);
                sqlobj.Add("parameters", null);
            }
            return sqlobj;

        }

    }
}

