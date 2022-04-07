using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class projectDuration
    {
        public int Id { get; set; }
        public int Duration { get; set; }

    internal AppDatabase Db { get; set; }

        public projectDuration()
        {

        }

        internal projectDuration(AppDatabase db)
        {
            Db = db;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName="@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter 
            { 
                ParameterName = "@duration",
                DbType = DbType.Int16,
                Value = Duration,
            });
        }
    }
}