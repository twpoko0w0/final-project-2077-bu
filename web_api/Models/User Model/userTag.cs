using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class userTag
    {
        public int Id { get; set; }
        public string User_tag_name { get; set; }

        internal AppDatabase Db { get; set; }

        public userTag()
        {

        }

        internal userTag(AppDatabase db)
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
                ParameterName = "@user_tag_name",
                DbType = DbType.String,
                Value = User_tag_name,
            });
        }
    }
}