using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using System;


namespace web_api
{
    public class projectQuestion
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public string Project_question { get; set; } 
        public string Project_answer { get; set; } 

    internal AppDatabase Db { get; set; }

        public projectQuestion()
        {

        }

        internal projectQuestion(AppDatabase db)
        {
            Db = db;
        }

        // Insert Data
        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `project_question`      (`project_id`,
                                                                    `project_question`,
                                                                    `project_answer`) 

                                                               VALUES (@project_id,
                                                                       @project_question,
                                                                       @project_answer);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `project_question` SET `project_id`= @project_id, 
                                                                  `project_question`= @project_question,
                                                                  `project_answer`= @project_answer  WHERE `Id`= @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `project_question` WHERE `Id` = @id;";
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
                ParameterName = "@project_question",
                DbType = DbType.String,
                Value = Project_question,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@project_answer",
                DbType = DbType.String,
                Value = Project_answer,
            });
        }
    }
}
