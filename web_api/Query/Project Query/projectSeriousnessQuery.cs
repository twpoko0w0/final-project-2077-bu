using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectSeriousnessQuery
    {
        public AppDatabase Db { get; }

        public projectSeriousnessQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectSeriousness> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_seriousness` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectSeriousness>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_seriousness` ORDER BY `id` DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectSeriousness>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectSeriousness>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectSeriousness(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_seriousness_name = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}