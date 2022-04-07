using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class userProjectRel
    {
        public int Id { get; set; }
        public string User_id { get; set; }
        public int Project_id { get; set; } 
        public int Project_role_id { get; set; } 
        public int Project_tag_rel_id { get; set; } 

    internal AppDatabase Db { get; set; }

        public userProjectRel()
        {

        }

        internal userProjectRel(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `user_project_relation` (`user_id`, 
                                                                    `project_id`,
                                                                    `project_role_id`,
                                                                    `project_tag_rel_id`) 

                                                               VALUES (@user_id,
                                                                       @project_id,
                                                                       @project_role_id,
                                                                       @project_tag_rel_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `user_project_relation` SET `user_id`= @user_id,  
                                                                  `project_id`= @project_id,  
                                                                  `project_role_id`= @project_role_id,
                                                                  `project_tag_rel_id`= project_tag_rel_id WHERE `Id`= @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `user_project_relation` WHERE `Id` = @id;";
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
                ParameterName = "@project_id",
                DbType = DbType.Int32,
                Value = Project_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_role_id",
                DbType = DbType.Int16,
                Value = Project_role_id,
            });
            
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_tag_rel_id",
                DbType = DbType.Int16,
                Value = Project_tag_rel_id,
            });
        }
    }
}
