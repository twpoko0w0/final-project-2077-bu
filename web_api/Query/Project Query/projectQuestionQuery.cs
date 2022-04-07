using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectQuestionQuery
    {
        public AppDatabase Db { get; }

        public projectQuestionQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectQuestion> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `project_id`, `project_question`, `project_answer` FROM project_question WHERE id = @id; ";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<projectQuestion>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `id`, `project_id`, `project_question`, `project_answer` FROM project_question ORDER BY id DESC; ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectQuestion>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectQuestion>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectQuestion(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_id = reader.GetInt32(1),
                        Project_question = reader.GetString(2),
                        Project_answer = reader.GetString(3),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}