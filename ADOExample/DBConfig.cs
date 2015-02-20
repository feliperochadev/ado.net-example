using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ADOExample
{
    public class DBConfig
    {
        public static IDbConnection Connection()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
            connection.Open();
            return connection;
        }

        public static IDbCommand Command(string query, IDbConnection connection)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = query;
            return command;
        }


        public static IDbDataParameter Parameter(string paramterName, object value)
        {
            return new SqlParameter(paramterName, value);
        }


        public static IDataAdapter Adapter(IDbCommand command)
        {
            IDbDataAdapter adap = new SqlDataAdapter();
            adap.SelectCommand = command;
            return adap;
        }
    }
}
