using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class userType
    {
        public int Id { get; set; }
        public string User_type { get; set; }

        internal AppDatabase Db { get; set; }

        public userType()
        {

        }

        internal userType(AppDatabase db)
        {
            Db = db;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_type",
                DbType = DbType.String,
                Value = User_type,
            });
        }
    }
}