using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class userProjectJoinReq
    {
        public int Id { get; set; }
        public string User_id { get; set; }
        public int Project_id { get; set; }
        public DateTime Date_time { get; set; }
        public int Project_tag_rel_id { get; set; }
        public string Interview { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public string Line { get; set; }

        internal AppDatabase Db { get; set; }

        public userProjectJoinReq()
        {

        }

        internal userProjectJoinReq(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `user_project_join_request` (`user_id`, 
                                                                        `project_id`,
                                                                        `date_time`,
                                                                        `project_tag_rel_id`,
                                                                        `interview`,
                                                                        `facebook`,
                                                                        `email`,
                                                                        `line`) 

                                                                 VALUES (@user_id,
                                                                         @project_id,
                                                                         @date_time,
                                                                         @project_tag_rel_id,
                                                                         @interview,
                                                                         @facebook,
                                                                         @email,
                                                                         @line);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `user_project_join_request` SET `user_id`= @user_id,  
                                                                       `project_id`= @project_id,
                                                                       `date_time`= @date_time,
                                                                       `project_tag_rel_id`= @project_tag_rel_id,
                                                                       `interview`= @interview,
                                                                       `facebook`= @facebook,
                                                                       `email`= @email,
                                                                       `line`= @line WHERE `Id`= @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `user_project_join_request` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
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
                ParameterName = "@user_id",
                DbType = DbType.String,
                Value = User_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_id",
                DbType = DbType.Int32,
                Value = Project_id,
            });
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@date_time",
                DbType = DbType.DateTime,
                Value = Date_time,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_tag_rel_id",
                DbType = DbType.Int32,
                Value = Project_tag_rel_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@interview",
                DbType = DbType.String,
                Value = Interview,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@facebook",
                DbType = DbType.String,
                Value = Facebook,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@email",
                DbType = DbType.String,
                Value = Email,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@line",
                DbType = DbType.String,
                Value = Line,
            });
        }
    }
}
