using Dapper;
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
        /// <summary>
        /// 执行增删改的操作
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param">参数对象</param>
        /// <returns>返回受影响行数</returns>
        protected int Execute(string sql, object param = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    int result = connection.Execute(sql, param);
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return 0;
        }

        /// <summary>
        /// 获取多条强类型数据，可被重写
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>强类型数据的List</returns>
        protected List<T> GetList(string sql, object param = null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<T>(sql, param).ToList();
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return null;
        }

        /// <summary>
        /// 返回单条强类型数据
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param">sql参数</param>
        /// <returns>强类型模型</returns>
        protected T GetModel(string sql,object param=null)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Config.connStr))
                {
                    return connection.Query<T>(sql, param) as T;
                }
            }
            catch (Exception ex)
            {
                //写入日志
            }
            return null;
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

        public virtual Dictionary<string, object> UpdateAssembleSql(T model)
        {
            Dictionary<string, object> sqlobj = new Dictionary<string, object>();
            if (model != null)
            {
                string sql = @"update ";
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
                        sqlSet += PropertyName + "=@" + PropertyName;
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

