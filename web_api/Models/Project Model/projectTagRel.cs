using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class projectTagRel
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int Project_tag_id { get; set; } 
        public string Project_tag_role { get; set; } 
        public int Project_position_quantity_id { get; set; } 

    internal AppDatabase Db { get; set; }

        public projectTagRel()
        {

        }

        internal projectTagRel(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `project_tag_relation` (`project_id`, 
                                                                    `project_tag_id`,
                                                                    `project_tag_role`,
                                                                    `project_position_quantity_id`) 

                                                               VALUES (@project_id,
                                                                       @project_tag_id,
                                                                       @project_tag_role,
                                                                       @project_position_quantity_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `project_tag_relation` SET `project_id`= @project_id,  
                                                                  `project_tag_id`= @project_tag_id,  
                                                                  `project_tag_role`= @project_tag_role,
                                                                  `project_position_quantity_id`= @project_position_quantity_id  WHERE `Id`= @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `project_tag_relation` WHERE `Id` = @id;";
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
                ParameterName = "@project_id",
                DbType = DbType.Int16,
                Value = Project_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_tag_id",
                DbType = DbType.Int16,
                Value = Project_tag_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_tag_role",
                DbType = DbType.String,
                Value = Project_tag_role,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_position_quantity_id",
                DbType = DbType.Int16,
                Value = Project_position_quantity_id,
            });
        }
    }
}
