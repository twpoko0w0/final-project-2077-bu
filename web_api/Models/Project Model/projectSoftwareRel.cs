using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class projectSoftwareRel
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int Project_software_id { get; set; } 


    internal AppDatabase Db { get; set; }

        public projectSoftwareRel()
        {

        }

        internal projectSoftwareRel(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `project_software_relation` (`project_id`, 
                                                                        `project_software_id`) 

                                                                 VALUES (@project_id,
                                                                         @project_software_id);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `project_software_relation` SET `project_id`= @project_id,  
                                                                       `project_software_id`= @project_software_id WHERE `Id`= @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `project_software_relation` WHERE `Id` = @id;";
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
                DbType = DbType.Int32,
                Value = Project_id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_software_id",
                DbType = DbType.Int16,
                Value = Project_software_id,
            });
        }
    }
}
