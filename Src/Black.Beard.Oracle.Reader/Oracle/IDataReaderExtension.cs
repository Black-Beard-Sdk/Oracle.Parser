using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Pssa.Sdk.DataAccess.Dao.Contracts
{

    public static class IDataReaderExtension
    {

        ///// <summary>
        ///// get the field specified by the index and cast to the generic type
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="self"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public static T Field<T>(this IDataReader self, int index)
        //{

        //    T result = default(T);

        //    var value = self[index];

        //    if (!(value is DBNull))
        //    {
        //        if (typeof(T) == value.GetType())
        //        {
        //            result = (T)value;
        //        }
        //        else
        //        {
        //            var n = self.GetName(index);
        //            throw new Exception(string.Format("the column {0} can't be casted from {1} to {2}", n, value.GetType().Name, typeof(T).Name));
        //        }
        //    }
        //    return result;
        //}


        public static T Field<T>(this IDataReader reader, int index)
        {
            T result = default(T);

            dynamic value = null;

            try
            {
                value = reader[index];
            }
            catch (Exception)
            {

            }

            if (!(value is DBNull))
            {
                try
                {
                    return Convert.ChangeType(value, typeof(T));
                }
                catch (Exception)
                {
                    throw new InvalidCastException(string.Format("invalid cast at column {2} from {0} to  {1}", value.GetType().FullName, typeof(T).FullName, reader.GetName(index)));
                }
                //if (value is T)
                //    result = (T)value;
                //else
                //    throw new InvalidCastException(string.Format("invalid cast at column {2} from {0} to  {1}", value.GetType().FullName, typeof(T).FullName, reader.GetName(index)));
            }

            return result;

        }


        /// <summary>
        /// get the field specified by the name and cast to the generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T Field<T>(this IDataReader self, string columnName)
        {

            T result = default(T);

            var value = self[columnName];

            if (!(value is DBNull))
                result = (T)value;

            return result;
        }

        /// <summary>
        /// get the field specified by the name and cast to the generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static Nullable<T> FieldOrNull<T>(this IDataReader self, string columnName)
            where T : struct
        {

            Nullable<T> result = new Nullable<T>();

            var value = self[columnName];

            if (!(value is DBNull))
                result = (T)value;

            return result;
        }


        /// <summary>
        /// get the field specified by the index and cast to the generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Nullable<T> FieldOrNull<T>(this IDataReader self, int index)
            where T : struct
        {

            Nullable<T> result = new Nullable<T>();

            var value = self[index];

            if (!(value is DBNull))
                result = (T)value;

            return result;
        }

        /// <summary>
        /// get the field specified by the name and cast to the generic type withe method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <param name="_f"></param>
        /// <returns></returns>
        public static T2 Field<T, T2>(this IDataReader self, string columnName, Func<T, T2> _f)
        {

            T result = default(T);

            var value = self[columnName];

            if (!(value is DBNull))
                result = (T)value;
            else
                return default(T2);

            return _f(result);

        }

        /// <summary>
        /// get the field specified by the name and cast to the generic type withe method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <param name="_f"></param>
        /// <returns></returns>
        public static Nullable<T2> FieldOrNull<T, T2>(this IDataReader self, string columnName, Func<Nullable<T>, Nullable<T2>> _f)
            where T : struct
            where T2 : struct
        {

            Nullable<T> result = new Nullable<T>();

            var value = self[columnName];

            if (!(value is DBNull))
                result = (T)value;
            else
                return default(T2);

            return _f(result);

        }

        /// <summary>
        /// get the field specified by the name and cast to the generic type withe method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="self"></param>
        /// <param name="columnName"></param>
        /// <param name="_f"></param>
        /// <returns></returns>
        public static T2 Field<T, T2>(this IDataReader self, int index, Func<T, T2> _f)
        {

            T result = default(T);

            var value = self[index];

            if (!(value is DBNull))
                result = (T)value;
            else
                return default(T2);

            return _f(result);

        }


        /// <summary>
        /// get the field specified by the name and cast to the generic type withe method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="self"></param>
        /// <param name="columnName"></param>
        /// <param name="_f"></param>
        /// <returns></returns>
        public static Nullable<T2> FieldOrNull<T, T2>(this IDataReader self, int index, Func<Nullable<T>, Nullable<T2>> _f)
            where T : struct
            where T2 : struct
        {

            Nullable<T> result = new Nullable<T>();

            var value = self[index];

            if (!(value is DBNull))
                result = (T)value;
            else
                return default(T2);

            return _f(result);

        }

        /// <summary>
        /// return true if the value specified by the name is DBNull
        /// </summary>
        /// <param name="self"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool IsNull(this IDataReader self, string columnName)
        {
            var value = self[columnName];
            return value is DBNull;
        }


    }

    public class Reader
    {

        private bool intialized;
        IDataReader reader;
        private Func<string, int> GetColumnName;
        private bool ignorecase;
        public Reader(IDataReader reader, bool ignorecase = false)
        {
            this.ignorecase = ignorecase;
            this.reader = reader;

            Initialize();

        }

        private void Initialize()
        {

            intialized = true;

            List<SwitchCase> lst = new List<SwitchCase>();
            var sourceParameterExpr = Expression.Parameter(typeof(string), "columnName");
            var result = Expression.Variable(typeof(int));
            //var _return = Expression.Return(null, result, typeof(int));

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var t1 = this.reader.GetName(i);
                if (ignorecase)
                    t1 = t1.ToLower();
                Debug.WriteLine(t1);
                SwitchCase c1 = Expression.SwitchCase(Expression.Assign(result, Expression.Constant(i)), Expression.Constant(t1, typeof(string)));
                lst.Add(c1);
            }

            var l = lst.ToArray();

            var sw = Expression.Switch(sourceParameterExpr, Expression.Assign(result, Expression.Constant(-1)), l);

            var blk = Expression.Block
                (new List<ParameterExpression>() { result },
                    sw,
                    result
                );

            var f = Expression.Lambda<Func<string, int>>
                (
                    blk
                    , sourceParameterExpr
                );

            this.GetColumnName = f.Compile();

        }

        public T Field<T>(string columnName)
        {

            T result = default(T);

            int index = this.GetColumnName(ignorecase ? columnName.ToLower() : columnName);

            if (index >= 0 && index < reader.FieldCount)
            {
                try
                {
                    var value = reader[index];
                    if (!(value is DBNull))
                        result = (T)value;
                }
                catch (IndexOutOfRangeException)
                {

                }

            }
            else
            {

            }

            return result;

        }

        public T2 Field<T, T2>(string columnName, Func<T, T2> _f)
        {

            T result = default(T);

            int index = this.GetColumnName(ignorecase ? columnName.ToLower() : columnName);

            var value = reader[index];

            if (!(value is DBNull))
                result = (T)value;
            else
                return default(T2);

            return _f(result);

        }

        public bool IsNull(string columnName)
        {
            int index = this.GetColumnName(ignorecase ? columnName.ToLower() : columnName);
            var value = reader[index];
            return value is DBNull;
        }

        public string GetName(int i)
        {
            return reader.GetName(i);
        }

        public int FieldCount { get { return reader.FieldCount; } }

        public Type GetFieldType(int i)
        {
            return reader.GetFieldType(i);
        }

        public object GetValue(int index)
        {
            return reader.GetValue(index);
        }

    }





}
