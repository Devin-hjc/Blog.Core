using System.IO;

namespace Blog.Core.Common.DB
{
    public class BaseDBConfig
    {
        private static string sqliteConnection = Appsettings.app(new string[] { "AppSettings", "Sqlite", "SqliteConnection" });
        private static bool isSqliteEnabled = (Appsettings.app(new string[] { "AppSettings", "Sqlite", "Enabled" })).ObjToBool();

        private static string sqlServerConnection = Appsettings.app(new string[] { "AppSettings", "SqlServer", "SqlServerConnection" });
        private static bool isSqlServerEnabled = (Appsettings.app(new string[] { "AppSettings", "SqlServer", "Enabled" })).ObjToBool();

        private static string mySqlConnection = Appsettings.app(new string[] { "AppSettings", "MySql", "MySqlConnection" });
        private static bool isMySqlEnabled = (Appsettings.app(new string[] { "AppSettings", "MySql", "Enabled" })).ObjToBool();

        private static string oracleConnection = Appsettings.app(new string[] { "AppSettings", "Oracle", "OracleConnection" });
        private static bool IsOracleEnabled = (Appsettings.app(new string[] { "AppSettings", "Oracle", "Enabled" })).ObjToBool();

        private static string sqlConnection = string.Empty;

        public static string ConnectionString => InitConn();
        public static DataBaseType DbType = DataBaseType.SqlServer;


        private static string InitConn()
        {
            if (!string.IsNullOrEmpty(sqlConnection)) {
                return sqlConnection;
            }

            if (isSqliteEnabled)
            {
                DbType = DataBaseType.Sqlite;
                sqlConnection = sqliteConnection;
            }
            else if (isSqlServerEnabled)
            {
                DbType = DataBaseType.SqlServer;
                //sqlConnection = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1.txt", @"c:\my-file\dbCountPsw1.txt", sqlServerConnection);
                sqlConnection = sqlServerConnection;
            }
            else if (isMySqlEnabled)
            {
                DbType = DataBaseType.MySql;
                //sqlConnection = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1_MySqlConn.txt", @"c:\my-file\dbCountPsw1_MySqlConn.txt", mySqlConnection);
                sqlConnection = mySqlConnection;
            }
            else if (IsOracleEnabled)
            {
                DbType = DataBaseType.Oracle;
                //sqlConnection = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1_OracleConn.txt", @"c:\my-file\dbCountPsw1_OracleConn.txt", oracleConnection);
                sqlConnection = oracleConnection;
            }
            //else
            //{
            //    return "server=.;uid=sa;pwd=sa;database=WMBlogDB";
            //}

            return sqlConnection;
        }
        private static string DifDBConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (System.Exception) { }
            }

            return conn[conn.Length - 1];
        }

    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
}
