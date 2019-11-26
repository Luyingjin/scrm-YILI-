﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pb.Hangfire.Tool
{
    public class MssqlHelper : IDBHelper
    {
        private string ConnectString { get; set; }
        public MssqlHelper(string connentString)
        {
            this.ConnectString = connentString;
        }

        #region +ExcuteNonQuery 增、删、改同步操作
        /// <summary>
        /// 增、删、改同步操作
        ///  </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>int</returns>
        public int ExcuteNonQuery(string cmd, DynamicParameters param=null, bool flag = true)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    result = con.Execute(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    result = con.Execute(cmd, param, null, null, CommandType.Text);
                }
            }
            return result;
        }
        #endregion

        #region +ExcuteNonQueryAsync 增、删、改异步操作
        /// <summary>
        /// 增、删、改异步操作
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>int</returns>
        public async Task<int> ExcuteNonQueryAsync(string cmd, DynamicParameters param = null, bool flag = true)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    result = await con.ExecuteAsync(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    result = await con.ExecuteAsync(cmd, param, null, null, CommandType.Text);
                }
            }
            return result;
        }
        #endregion

        #region +ExecuteScalar 同步查询操作
        /// <summary>
        /// 同步查询操作
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>object</returns>
        public object ExecuteScalar(string cmd, DynamicParameters param = null, bool flag = true)
        {
            object result = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    result = con.ExecuteScalar(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    result = con.ExecuteScalar(cmd, param, null, null, CommandType.Text);
                }
            }
            return result;
        }
        #endregion

        #region +ExecuteScalarAsync 异步查询操作
        /// <summary>
        /// 异步查询操作
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>object</returns>
        public async Task<object> ExecuteScalarAsync(string cmd, DynamicParameters param = null, bool flag = true)
        {
            object result = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    result = await con.ExecuteScalarAsync(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    result = await con.ExecuteScalarAsync(cmd, param, null, null, CommandType.Text);
                }
            }
            return result;
        }
        #endregion

        #region +FindOne  同步查询一条数据
        /// <summary>
        /// 同步查询一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>t</returns>
        public T FindOne<T>(string cmd, DynamicParameters param = null, bool flag = true) where T : class, new()
        {
            IDataReader dataReader = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    dataReader = con.ExecuteReader(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    dataReader = con.ExecuteReader(cmd, param, null, null, CommandType.Text);
                }
                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                T t = new T();
                foreach (var item in type.GetProperties())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                        var kvalue = dataReader[item.Name];
                        if (kvalue == DBNull.Value) continue;
                        item.SetValue(t, kvalue, null);
                        break;
                    }
                }
                return t;
            }
        }
        #endregion

        #region +FindOne  异步查询一条数据
        /// <summary>
        /// 异步查询一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>t</returns>
        public async Task<T> FindOneAsync<T>(string cmd, DynamicParameters param = null, bool flag = true) where T : class, new()
        {
            IDataReader dataReader = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.Text);
                }
                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                T t = new T();
                foreach (var item in type.GetProperties())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //属性名与查询出来的列名比较
                        if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                        var kvalue = dataReader[item.Name];
                        if (kvalue == DBNull.Value) continue;
                        item.SetValue(t, kvalue, null);
                        break;
                    }
                }
                return t;
            }
        }
        #endregion

        #region +FindToList  同步查询数据集合
        /// <summary>
        /// 同步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>t</returns>
        public IList<T> FindToList<T>(string cmd, DynamicParameters param = null, bool flag = true) where T : class, new()
        {
            IDataReader dataReader = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    dataReader = con.ExecuteReader(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    dataReader = con.ExecuteReader(cmd, param, null, null, CommandType.Text);
                }
                if (dataReader == null) {
                    return new List<T>();
                }
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion

        #region +FindToListAsync  异步查询数据集合
        /// <summary>
        /// 异步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>t</returns>
        public async Task<IList<T>> FindToListAsync<T>(string cmd, DynamicParameters param = null, bool flag = true) where T : class, new()
        {
            IDataReader dataReader = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    dataReader = await con.ExecuteReaderAsync(cmd, param, null, null, CommandType.Text);
                }
                if (dataReader == null)
                {
                    return new List<T>();
                }
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion

        #region +FindToList  同步查询数据集合
        /// <summary>
        /// 同步查询数据集合
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="cmd">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="flag">true存储过程，false sql语句</param>
        /// <returns>t</returns>
        public IList<T> FindToListAsPage<T>(string cmd, DynamicParameters param = null, bool flag = true) where T : class, new()
        {
            IDataReader dataReader = null;
            using (SqlConnection con = new SqlConnection(ConnectString))
            {
                if (flag)
                {
                    dataReader = con.ExecuteReader(cmd, param, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    dataReader = con.ExecuteReader(cmd, param, null, null, CommandType.Text);
                }
                if (dataReader == null || !dataReader.Read()) return null;
                Type type = typeof(T);
                List<T> tlist = new List<T>();
                while (dataReader.Read())
                {
                    T t = new T();
                    foreach (var item in type.GetProperties())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            //属性名与查询出来的列名比较
                            if (item.Name.ToLower() != dataReader.GetName(i).ToLower()) continue;
                            var kvalue = dataReader[item.Name];
                            if (kvalue == DBNull.Value) continue;
                            item.SetValue(t, kvalue, null);
                            break;
                        }
                    }
                    if (tlist != null) tlist.Add(t);
                }
                return tlist;
            }
        }
        #endregion
    }
}
