using System;
using System.Data;
using Oracle.DataAccess.Client;

namespace Library.Scan
{
    public class DbObject : IDisposable
    {
        protected OracleConnection Cnn;
        private string _conStr;
        private OracleException _mDbObjectError;
        protected bool Disposed;

        public DbObject()
        {
            Cnn = new OracleConnection();
            OpenConnection();
        }

        private void OpenConnection()
        {
            Cnn = new OracleConnection();
            //_conStr = "Data Source=ORCL;User Id=ARTSCAN;Password=ARTSCAN;";
           // _conStr = "Data Source=SAMGAZ64;User Id=ARTSCAN;Password=ARTSCAN;";
          // _conStr = "Data Source=ARTSCAN;User Id=ARTSCAN;Password=ARTSCAN;";
          // _conStr = "Data Source=SYS;User Id=ARTSCAN;Password=ARTSCAN;";
            _conStr = "Data Source=ARTDB;User Id=SAMTARA;Password=SAMTARA;";

            try
            {
                Cnn.ConnectionString = _conStr;
                Cnn.Open();
            }
            catch (Exception)
            {                
                Cnn = null;
                throw;
            }
        }

        ~DbObject()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;
            if (disposing)
            {
                Cnn.Close();
                Cnn.Dispose();
            }
            Disposed = true;
        }

        public OracleConnection Connection { get { return Cnn; } }

        public OracleDataReader Reader(string sql, Parameter[] parameters)
        {
            if ((Cnn.State != ConnectionState.Open))
            {
                OpenConnection();
            }
            var cmd = new OracleCommand();
            cmd.Connection = Cnn;
            cmd.CommandText = sql;

            for (int i = 0; parameters != null && i < parameters.Length; i++)
                cmd.Parameters.Add(new OracleParameter(parameters[i].Name, parameters[i].Value));

            try
            {
                OracleDataReader rd = cmd.ExecuteReader();
                cmd.Dispose();
                return rd;
            }
            catch (OracleException ex)
            {
                _mDbObjectError = ex;
                return null;
            }
        }

        public DataSet DataSet(string mSql, Parameter[] parameters = null)
        {
            if (Cnn != null)
            {
                if (Cnn.State != ConnectionState.Open)
                {
                    OpenConnection();
                }

                var cmd = new OracleCommand();
                cmd.Connection = Cnn;
                cmd.CommandText = mSql;

                for (var i = 0; parameters != null && i < parameters.Length; i++)
                    if (parameters[i] != null)
                        cmd.Parameters.Add(new OracleParameter(parameters[i].Name, parameters[i].Value));

                var da = new OracleDataAdapter();
                da.SelectCommand = cmd;
                var ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return null;
                return ds;
            }

            return null;
        }

        public int Execute(string sql, Parameter[] parameters)
        {
            if (Cnn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            var cmd = new OracleCommand();
            cmd.Connection = Cnn;
            cmd.CommandText = sql;

            for (int i = 0; parameters != null && i < parameters.Length; i++)
                if (parameters[i] != null)
                    cmd.Parameters.Add(new OracleParameter(parameters[i].Name, parameters[i].Value));

            var result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return result;
        }

        public int ExecuteScalar(string sql, Parameter[] parameters)
        {
            if (Cnn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            var cmd = new OracleCommand();
            cmd.Connection = Cnn;
            cmd.CommandText = sql;

            for (int i = 0; parameters != null && i < parameters.Length; i++)
                cmd.Parameters.Add(new OracleParameter(parameters[i].Name, parameters[i].Value));

            var result = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            return result;
        }

        public object GetColValue(string tableName, string fieldName, string whereClause, Parameter[] parameters)
        {
            object retVal = null;
            string sql = "Select " + fieldName + " from " + tableName + " Where " + whereClause;
            OracleDataReader rd = Reader(sql, parameters);
            if (rd.HasRows)
            {
                retVal = rd.GetValue(0);
            }
            rd.Close();
            return retVal;
        }

        public int GetSequence(string tableName)
        {
            DataSet ds = DataSet("SELECT " + tableName + "_SEQ.NEXTVAL AS TABLOSEQ FROM DUAL", null);
            object seq = ds.Tables[0].Rows[0]["TABLOSEQ"];

            if (seq != null)
                return Convert.ToInt32(seq);
            return 0;
        }

        public int InsertRecord(System.Collections.ArrayList fieldList, System.Collections.ArrayList valueList, string tableName)
        {
            int i;
            var sql = "";
            sql += "Insert Into " + tableName + " (";
            for (i = 0; i < fieldList.Count; i++)
            {
                sql += fieldList[i] + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ") Values(";
            var parameters = new Parameter[valueList.Count];
            for (i = 0; i < valueList.Count; i++)
            {
                sql += " :param"+i+" ,";
                parameters[i] = new Parameter("param" +i, valueList[i]);
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            return ExecuteScalar(sql, parameters);
        }

        public int UpdateRecord(System.Collections.ArrayList fieldList, System.Collections.ArrayList valueList, string tableName, string whereString, Parameter[] whereParameters)
        {
            string sql = "";
            sql += "Update " + tableName + " Set ";
            int whereParametersCount = 0;
            if (whereParameters != null)
                whereParametersCount = whereParameters.Length;
            var parameters = new Parameter[valueList.Count + whereParametersCount];
            for (int i = 0; i < fieldList.Count; i++)
            {
                sql += fieldList[i] + " = :param" + i+" ,";
                parameters[i] = new Parameter("param" + i, valueList[i]);
            }
            //Virgülü Kaldýr
            sql = sql.Substring(0, sql.Length - 1);
            sql += " Where " + whereString;
            for (int i = 0; i < whereParametersCount; i++)
                if (whereParameters != null && whereParameters[i] != null)
                    parameters[fieldList.Count + i] = whereParameters[i];
            return Execute(sql, parameters);
        }

        public OracleException DBObjectError { get { return _mDbObjectError; } }



        //#endregion


        #region PUBLIC STATIC FUNCTIONS

        public static bool GetBool(OracleDataReader dr, string fieldName)
        {
            bool res = false;
            try
            {
                if (dr.IsDBNull(dr.GetOrdinal(fieldName)))
                    return false;

                res = dr.GetBoolean(dr.GetOrdinal(fieldName));
            }
            catch
            { }
            return res;
        }

        public static string getStr(OracleDataReader dr, string fieldName)
        {
            string res = "";
            try
            {
                if (dr.IsDBNull(dr.GetOrdinal(fieldName)))
                    return "";

                res = dr.GetString(dr.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                res = "";
            }
            return res;
        }

        public static string getValue(OracleDataReader dr, string fieldName)
        {
            string res = "";
            try
            {
                if (dr.IsDBNull(dr.GetOrdinal(fieldName)))
                    return "";

                res = dr.GetValue(dr.GetOrdinal(fieldName)).ToString();
            }
            catch (Exception ex)
            {
                res = "";
            }
            return res;
        }

        public static string getStr(DataRow drow, string field)
        {
            string res;
            try
            {
                res = System.Convert.ToString(drow[field]);
            }
            catch (Exception ex)
            {
                res = "";
            }
            return res;
        }

        public static DateTime getDate(OracleDataReader dr, string fieldName)
        {
            DateTime res;
            try
            {
                res = dr.GetDateTime(dr.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {

                return System.DateTime.MinValue;
            }

            return res;
        }

        public static DateTime getDate(DataRow drow, string field)
        {
            DateTime res;
            try
            {
                res = (DateTime)drow[field];
            }
            catch (Exception ex)
            {

                return System.DateTime.MinValue;
            }

            return res;
        }
        public static string GetDateStr(DataRow drow, string field, bool withTime)
        {
            DateTime res;
            string resStr;
            try
            {
                if (drow[field] == System.DBNull.Value)
                    return "";

                res = (DateTime)drow[field];

                resStr = !withTime ? res.ToShortDateString() : res.ToString();

            }
            catch (Exception ex)
            {
                return "HATA"; //System.DateTime.MinValue;
            }

            return resStr;
        }

        public static int getInt(DataRow drow, string field)
        {
            int res;
            try
            {
                if (drow[field] == System.DBNull.Value)
                {
                    return 0;
                }
                res = System.Convert.ToInt32(drow[field]);
            }
            catch (Exception ex)
            {

                res = -1;
            }
            return res;

        }

        public static decimal getDecimal(OracleDataReader dr, string fieldName)
        {
            decimal res;
            try
            {
                res = dr.GetDecimal(dr.GetOrdinal(fieldName));
            }
            catch (Exception ex)
            {
                return 0;
            }

            return res;
        }

        public static int getBit(OracleDataReader dr, string fieldName)
        {
            int res = 0;

            try
            {
                if (dr.IsDBNull(dr.GetOrdinal(fieldName)))
                    return 0;

                res = Convert.ToInt16(dr[fieldName]);

            }
            catch (Exception ex)
            {
                //if (ex.Number == 1)
                res = -1;

            }
            return res;
        }

        public static int getInt(OracleDataReader dr, string fieldName)
        {
            int res = 0;
            try
            {
                if (dr.IsDBNull(dr.GetOrdinal(fieldName)))
                    return 0;

                res = dr.GetInt32(dr.GetOrdinal(fieldName));

            }
            catch (Exception ex)
            {
                //if (ex.Number == 1)
                return 0;


            }
            return res;
        }


        #endregion
    }
}