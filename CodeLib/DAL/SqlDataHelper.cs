using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CodeLib.DAL
{
    public class SqlDataHelper
    {
        public static string GetConnectionString()
        {
            string connStringName = (CommonObjects.IsAppInPROD) ? "UserPortal" : "UserPortal"; // Put TEST database name here
            return System.Configuration.ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
        }

        public static SqlParameter GetParam(string paramName, SqlDbType sqlType, object value)
        {
            SqlParameter res = new SqlParameter();
            res.ParameterName = paramName;
            res.SqlDbType = sqlType;
            res.Value = value;
            return res;
        }

        #region Get Value Methods

        public static T GetDataReaderValue<T>(IDataReader rdr, string fieldName)
        {
            string error = null;
            return GetDataReaderValue<T>(rdr, fieldName, out error);
        }

        public static T GetDataReaderValue<T>(IDataReader rdr, string fieldName, out string error)
        {
            T objOut = default(T);
            Type type = typeof(T);
            error = null;

            if (rdr != null)
            {
                try
                {
                    if (rdr[fieldName] != DBNull.Value)
                    {
                        if (type == typeof(bool))
                        {
                            bool elementBool;
                            objOut = (T)(object)(bool.TryParse(rdr[fieldName].ToString(), out elementBool) ? elementBool : false);
                        }
                        else if (type == typeof(bool?))
                        {
                            bool elementBool;
                            objOut = (T)(object)(bool.TryParse(rdr[fieldName].ToString(), out elementBool) ? new bool?(elementBool) : null);
                        }
                        else if (type == typeof(byte))
                        {
                            byte elementByte;
                            objOut = (T)(object)(byte.TryParse(rdr[fieldName].ToString(), out elementByte) ? elementByte : (byte)0);
                        }
                        else if (type == typeof(byte?))
                        {
                            byte elementByte;
                            objOut = (T)(object)(byte.TryParse(rdr[fieldName].ToString(), out elementByte) ? new byte?(elementByte) : null);
                        }
                        else if (type == typeof(short))
                        {
                            short elementShort;
                            objOut = (T)(object)(short.TryParse(rdr[fieldName].ToString(), out elementShort) ? elementShort : (short)0);
                        }
                        else if (type == typeof(short?))
                        {
                            short elementShort;
                            objOut = (T)(object)(short.TryParse(rdr[fieldName].ToString(), out elementShort) ? new short?(elementShort) : null);
                        }
                        else if (type == typeof(int))
                        {
                            int elementInt;
                            objOut = (T)(object)(int.TryParse(rdr[fieldName].ToString(), out elementInt) ? elementInt : (int)0);
                        }
                        else if (type == typeof(int?))
                        {
                            int elementInt;
                            objOut = (T)(object)(int.TryParse(rdr[fieldName].ToString(), out elementInt) ? new int?(elementInt) : null);
                        }
                        else if (type == typeof(long))
                        {
                            long elementLong;
                            objOut = (T)(object)(long.TryParse(rdr[fieldName].ToString(), out elementLong) ? elementLong : (long)0);
                        }
                        else if (type == typeof(long?))
                        {
                            long elementLong;
                            objOut = (T)(object)(long.TryParse(rdr[fieldName].ToString(), out elementLong) ? new long?(elementLong) : null);
                        }
                        else if (type == typeof(decimal))
                        {
                            decimal elementDcml;
                            objOut = (T)(object)(decimal.TryParse(rdr[fieldName].ToString(), out elementDcml) ? elementDcml : (decimal)0);
                        }
                        else if (type == typeof(decimal?))
                        {
                            decimal elementDcml;
                            objOut = (T)(object)(decimal.TryParse(rdr[fieldName].ToString(), out elementDcml) ? new decimal?(elementDcml) : null);
                        }
                        else if (type == typeof(float))
                        {
                            float elementFloat;
                            objOut = (T)(object)(float.TryParse(rdr[fieldName].ToString(), out elementFloat) ? elementFloat : (float)0);
                        }
                        else if (type == typeof(float?))
                        {
                            float elementFloat;
                            objOut = (T)(object)(float.TryParse(rdr[fieldName].ToString(), out elementFloat) ? new float?(elementFloat) : null);
                        }
                        else if (type == typeof(double))
                        {
                            double elementDbl;
                            objOut = (T)(object)(double.TryParse(rdr[fieldName].ToString(), out elementDbl) ? elementDbl : (double)0);
                        }
                        else if (type == typeof(double?))
                        {
                            double elementDbl;
                            objOut = (T)(object)(double.TryParse(rdr[fieldName].ToString(), out elementDbl) ? new double?(elementDbl) : null);
                        }
                        else if (type == typeof(DateTime))
                        {
                            DateTime elementDT;
                            objOut = (T)(object)(DateTime.TryParse(rdr[fieldName].ToString(), out elementDT) ? elementDT : DateTime.MinValue);
                        }
                        else if (type == typeof(DateTime?))
                        {
                            DateTime elementDT;
                            objOut = (T)(object)(DateTime.TryParse(rdr[fieldName].ToString(), out elementDT) ? new DateTime?(elementDT) : null);
                        }
                        else if (type == typeof(char))
                        {
                            char elementChar;
                            objOut = (T)(object)(char.TryParse(rdr[fieldName].ToString(), out elementChar) ? elementChar : (char)0);
                        }
                        else if (type == typeof(char?))
                        {
                            char elementChar;
                            objOut = (T)(object)(char.TryParse(rdr[fieldName].ToString(), out elementChar) ? new char?(elementChar) : null);
                        }
                        else    // default to string
                        {
                            objOut = (T)(object)rdr[fieldName].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return objOut;
        }

        public static T GetDataRowValue<T>(DataRow row, string fieldName)
        {
            string error = null;
            return GetDataRowValue<T>(row, fieldName, out error);
        }

        public static T GetDataRowValue<T>(DataRow row, string fieldName, out string error)
        {
            T objOut = default(T);
            Type type = typeof(T);
            error = null;

            if (row != null)
            {
                try
                {
                    if (row[fieldName] != DBNull.Value)
                    {
                        if (type == typeof(bool))
                        {
                            bool elementBool;
                            objOut = (T)(object)(bool.TryParse(row[fieldName].ToString(), out elementBool) ? elementBool : false);
                        }
                        else if (type == typeof(bool?))
                        {
                            bool elementBool;
                            objOut = (T)(object)(bool.TryParse(row[fieldName].ToString(), out elementBool) ? new bool?(elementBool) : null);
                        }
                        else if (type == typeof(byte))
                        {
                            byte elementByte;
                            objOut = (T)(object)(byte.TryParse(row[fieldName].ToString(), out elementByte) ? elementByte : (byte)0);
                        }
                        else if (type == typeof(byte?))
                        {
                            byte elementByte;
                            objOut = (T)(object)(byte.TryParse(row[fieldName].ToString(), out elementByte) ? new byte?(elementByte) : null);
                        }
                        else if (type == typeof(short))
                        {
                            short elementShort;
                            objOut = (T)(object)(short.TryParse(row[fieldName].ToString(), out elementShort) ? elementShort : (short)0);
                        }
                        else if (type == typeof(short?))
                        {
                            short elementShort;
                            objOut = (T)(object)(short.TryParse(row[fieldName].ToString(), out elementShort) ? new short?(elementShort) : null);
                        }
                        else if (type == typeof(int))
                        {
                            int elementInt;
                            objOut = (T)(object)(int.TryParse(row[fieldName].ToString(), out elementInt) ? elementInt : (int)0);
                        }
                        else if (type == typeof(int?))
                        {
                            int elementInt;
                            objOut = (T)(object)(int.TryParse(row[fieldName].ToString(), out elementInt) ? new int?(elementInt) : null);
                        }
                        else if (type == typeof(long))
                        {
                            long elementLong;
                            objOut = (T)(object)(long.TryParse(row[fieldName].ToString(), out elementLong) ? elementLong : (long)0);
                        }
                        else if (type == typeof(long?))
                        {
                            long elementLong;
                            objOut = (T)(object)(long.TryParse(row[fieldName].ToString(), out elementLong) ? new long?(elementLong) : null);
                        }
                        else if (type == typeof(decimal))
                        {
                            decimal elementDcml;
                            objOut = (T)(object)(decimal.TryParse(row[fieldName].ToString(), out elementDcml) ? elementDcml : (decimal)0);
                        }
                        else if (type == typeof(decimal?))
                        {
                            decimal elementDcml;
                            objOut = (T)(object)(decimal.TryParse(row[fieldName].ToString(), out elementDcml) ? new decimal?(elementDcml) : null);
                        }
                        else if (type == typeof(float))
                        {
                            float elementFloat;
                            objOut = (T)(object)(float.TryParse(row[fieldName].ToString(), out elementFloat) ? elementFloat : (float)0);
                        }
                        else if (type == typeof(float?))
                        {
                            float elementFloat;
                            objOut = (T)(object)(float.TryParse(row[fieldName].ToString(), out elementFloat) ? new float?(elementFloat) : null);
                        }
                        else if (type == typeof(double))
                        {
                            double elementDbl;
                            objOut = (T)(object)(double.TryParse(row[fieldName].ToString(), out elementDbl) ? elementDbl : (double)0);
                        }
                        else if (type == typeof(double?))
                        {
                            double elementDbl;
                            objOut = (T)(object)(double.TryParse(row[fieldName].ToString(), out elementDbl) ? new double?(elementDbl) : null);
                        }
                        else if (type == typeof(DateTime))
                        {
                            DateTime elementDT;
                            objOut = (T)(object)(DateTime.TryParse(row[fieldName].ToString(), out elementDT) ? elementDT : DateTime.MinValue);
                        }
                        else if (type == typeof(DateTime?))
                        {
                            DateTime elementDT;
                            objOut = (T)(object)(DateTime.TryParse(row[fieldName].ToString(), out elementDT) ? new DateTime?(elementDT) : null);
                        }
                        else if (type == typeof(char))
                        {
                            char elementChar;
                            objOut = (T)(object)(char.TryParse(row[fieldName].ToString(), out elementChar) ? elementChar : (char)0);
                        }
                        else if (type == typeof(char?))
                        {
                            char elementChar;
                            objOut = (T)(object)(char.TryParse(row[fieldName].ToString(), out elementChar) ? new char?(elementChar) : null);
                        }
                        else    // default to string
                        {
                            objOut = (T)(object)row[fieldName].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }

            return objOut;
        }

        #endregion

        public static bool RowHasColumn(System.Data.DataRow row, string columnName)
        {
            DataColumnCollection columns = row.Table.Columns;

            if (columns.Contains(columnName))
                return true;
            else
                return false;
        }
    }

    public static class SqlReaderHelper
    {
        public static bool HasColumn(this SqlDataReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }

}
