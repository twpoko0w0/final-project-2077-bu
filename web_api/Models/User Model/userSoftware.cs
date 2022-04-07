using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class userSoftware
    {
        public int Id { get; set; }
        public string User_software { get; set; }
        public string Software_image_link { get; set; }

        internal AppDatabase Db { get; set; }

        public userSoftware()
        {

        }

        internal userSoftware(AppDatabase db)
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
                ParameterName = "@user_software",
                DbType = DbType.String,
                Value = User_software,
            });
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@software_image_link",
                DbType = DbType.String,
                Value = Software_image_link,
            });
        }
    }
}