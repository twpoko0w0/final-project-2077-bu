using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace web_api
{
    public class projectStatusQuery
    {
        public AppDatabase Db { get; }

        public projectStatusQuery(AppDatabase db)
        {
            Db = db;
        }

        public async Task<projectStatus> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_status` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });

            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0]: null;
        }

        public async Task<List<projectStatus>> LatestPostAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `project_status` ORDER BY `id` DESC;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<projectStatus>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<projectStatus>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new projectStatus(Db)
                    {
                        Id = reader.GetInt32(0),
                        Project_status_name = reader.GetString(1),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}