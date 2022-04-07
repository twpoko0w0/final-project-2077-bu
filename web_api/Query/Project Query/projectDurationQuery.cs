using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectDurationQuery
    {
        public AppDatabase Db { get; }

        public projectDurationQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectDuration> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_duration` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectDuration>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_duration` ORDER BY `id` DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectDuration>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectDuration>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectDuration(Db)
                    {
                        Id = reader.GetInt32(0),
                        Duration = reader.GetInt16(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}