using System;
using MySqlConnector;

namespace web_api
{
    public class AppDatabase: IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDatabase(string ConnectionString)
        {
            Connection = new MySqlConnection(ConnectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}