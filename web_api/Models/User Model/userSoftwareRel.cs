using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class userSoftwareRel
    {
        public int Id { get; set; }
        public string User_id { get; set; }
        public int User_software_id { get; set; } 


    internal AppDatabase Db { get; set; }

        public userSoftwareRel()
        {

        }

        internal userSoftwareRel(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `user_software_relation` (`user_id`, 
                                                                      `user_software_id`) 

                                                               VALUES (@user_id,
                                                                       @user_software_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `user_software_relation` SET `user_id`= @user_id,  
                                                                    `user_software_id`= @user_software_id WHERE `Id`= @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `user_software_relation` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
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
                ParameterName = "@user_id",
                DbType = DbType.String,
                Value = User_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@user_software_id",
                DbType = DbType.Int16,
                Value = User_software_id,
            });
        }
    }
}
